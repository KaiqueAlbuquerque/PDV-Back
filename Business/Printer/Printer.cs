// ***********************************************************************
// Assembly         : Vip.Printer
// Author           : Leandro Ferreira
// Created          : 16-03-2019
//
// ***********************************************************************
// <copyright file="Printer.cs" company="VIP Soluções">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2019 VIP Soluções
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Business.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Vip.Printer.Enums;
using Vip.Printer.EscBemaCommands;
using Vip.Printer.EscPosCommands;
using Vip.Printer.Helper;
using Vip.Printer.Interfaces.Command;
using Vip.Printer.Interfaces.Printer;

namespace Vip.Printer
{
    public class Printer : IPrinter
    {
        #region Properties

        private byte[] _buffer;
        private readonly string _printerName;
        private readonly IPrintCommand _command;
        private readonly PrinterType _printerType;

        #endregion

        #region Constructors

        public Printer(string printerName, PrinterType type)
        {
            _printerName = string.IsNullOrEmpty(printerName) ? "temp.prn" : printerName.Trim();
            _printerType = type;

            switch (type)
            {
                case PrinterType.Epson:
                    _command = new EscPos();
                    break;
                case PrinterType.Bematech:
                    _command = new EscBema();
                    break;
            }
        }

        #endregion

        #region Methods

        public int ColsNomal => _command.ColsNomal;
        public int ColsCondensed => _command.ColsCondensed;
        public int ColsExpanded => _command.ColsExpanded;

        public void PrintDocument()
        {
            if (_buffer == null)
                return;

            if (!RawPrinterHelper.SendBytesToPrinter(_printerName, _buffer))
                throw new ArgumentException("Não foi possível acessar a impressora: " + _printerName);
        }

        public void Append(string value)
        {
            AppendString(value, true);
        }

        public void Append(byte[] value)
        {
            if (value == null)
                return;

            var list = new List<byte>();
            if (_buffer != null) list.AddRange(_buffer);
            list.AddRange(value);
            _buffer = list.ToArray();
        }

        public void AppendWithoutLf(string value)
        {
            AppendString(value, false);
        }

        private void AppendString(string value, bool useLf)
        {
            if (string.IsNullOrEmpty(value))
                return;

            if (useLf) value += "\n";

            var list = new List<byte>();
            if (_buffer != null) list.AddRange(_buffer);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var bytes = _printerType == PrinterType.Bematech
                ? Encoding.GetEncoding(850).GetBytes(value)
                : Encoding.GetEncoding("IBM860").GetBytes(value);

            list.AddRange(bytes);
            _buffer = list.ToArray();
        }

        public void NewLine()
        {
            Append("\r");
        }

        public void NewLines(int lines)
        {
            for (var i = 1; i < lines; i++)
                NewLine();
        }

        public void Clear()
        {
            _buffer = null;
        }

        #endregion

        #region Commands

        public void Separator()
        {
            Append(_command.Separator());
        }

        public void AutoTest()
        {
            Append(_command.AutoTest());
        }

        public void MeuSeparator()
        {
            NormalWidth();
            Append("--------------------------------------------------------");
        }

        public void PrintOut(Sale sale)
        {
            DoubleWidth2();
            AlignCenter();
            Append("Caparroz Formas");
            NormalWidth();
            Append("Rua Pedro de Castro Velho, nº 915");
            Append("Jardim Iva - São Paulo - SP");
            MeuSeparator();
            DoubleWidth3();
            AlignCenter();
            Append("CUPOM NAO FISCAL");
            AlignLeft();
            MeuSeparator();
            NormalWidth();
            BoldMode($"Data: {DateTime.Now}");
            MeuSeparator();
            BoldMode($"Item  Cód.  Qtd  Descricao         Unit         Total");

            int increment = 1;
            foreach (var product in sale.ProductSales)
            {
                Append($"{increment.ToString().PadLeft(4, '0')}  {product.Product.ExternalId}  {product.Quantity.ToString().PadRight(4)} {product.Product.Description.PadRight(17)} R$ {product.Product.Value.ToString().PadRight(9)} R$ {product.Total}");
                increment++;
            }
            
            MeuSeparator();
            Append($"Forma pgto:                                  Dinheiro");
            Append($"VALOR TOTAL:                                 R$ {sale.TotalValue}");
            Append($"VALOR PAGO:                                  R$ {sale.AmountPaid}");
            Append($"TROCO:                                       R$ {sale.Change}");
        }

        #region FontMode

        public void ItalicMode(string value)
        {
            Append(_command.FontMode.Italic(value));
        }

        public void ItalicMode(PrinterModeState state)
        {
            Append(_command.FontMode.Italic(state));
        }

        public void BoldMode(string value)
        {
            Append(_command.FontMode.Bold(value));
        }

        public void BoldMode(PrinterModeState state)
        {
            Append(_command.FontMode.Bold(state));
        }

        public void UnderlineMode(string value)
        {
            Append(_command.FontMode.Underline(value));
        }

        public void UnderlineMode(PrinterModeState state)
        {
            Append(_command.FontMode.Underline(state));
        }

        public void ExpandedMode(string value)
        {
            Append(_command.FontMode.Expanded(value));
        }

        public void ExpandedMode(PrinterModeState state)
        {
            Append(_command.FontMode.Expanded(state));
        }

        public void CondensedMode(string value)
        {
            Append(_command.FontMode.Condensed(value));
        }

        public void CondensedMode(PrinterModeState state)
        {
            Append(_command.FontMode.Condensed(state));
        }

        #endregion

        #region FontWidth

        public void NormalWidth()
        {
            Append(_command.FontWidth.Normal());
        }

        public void DoubleWidth2()
        {
            Append(_command.FontWidth.DoubleWidth2());
        }

        public void DoubleWidth3()
        {
            Append(_command.FontWidth.DoubleWidth3());
        }

        #endregion

        #region Alignment

        public void AlignLeft()
        {
            Append(_command.Alignment.Left());
        }

        public void AlignRight()
        {
            Append(_command.Alignment.Right());
        }

        public void AlignCenter()
        {
            Append(_command.Alignment.Center());
        }

        #endregion

        #region PaperCut

        public void FullPaperCut()
        {
            Append(_command.PaperCut.Full());
        }

        public void FullPaperCut(bool predicate)
        {
            if (predicate)
                FullPaperCut();
        }

        public void PartialPaperCut()
        {
            Append(_command.PaperCut.Partial());
        }

        public void PartialPaperCut(bool predicate)
        {
            if (predicate)
                PartialPaperCut();
        }

        #endregion

        #region Drawer

        public void OpenDrawer()
        {
            Append(_command.Drawer.Open());
        }

        #endregion

        #region QrCode

        public void QrCode(string qrData)
        {
            Append(_command.QrCode.Print(qrData));
        }

        public void QrCode(string qrData, QrCodeSize qrCodeSize)
        {
            Append(_command.QrCode.Print(qrData, qrCodeSize));
        }

        #endregion

        #region BarCode

        public void Code128(string code)
        {
            Append(_command.BarCode.Code128(code));
        }

        public void Code39(string code)
        {
            Append(_command.BarCode.Code39(code));
        }

        public void Ean13(string code)
        {
            Append(_command.BarCode.Ean13(code));
        }

        #endregion

        #region InitializePrint

        public void InitializePrint()
        {
            RawPrinterHelper.SendBytesToPrinter(_printerName, _command.InitializePrint.Initialize());
        }

        #endregion

        #endregion
    }
}