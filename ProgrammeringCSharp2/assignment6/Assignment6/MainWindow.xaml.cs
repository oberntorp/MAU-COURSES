using Assignment6.InvoiceInformation;
using Assignment6.Validators;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InvoiceMapper invoiceMapper;
        private FileReader fileReader;
        private Invoice invoice;
        private InvoiceCalculator invoiceCalculator;
        private NumberValidator numberValidator;
        private DateValidator dateValidator;
        private double totalInvoicePrice = 0;  

        /// <summary>
        /// Constructor for MainWindow, adds dependencies
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            invoiceCalculator = new InvoiceCalculator();
            numberValidator = new NumberValidator();
            dateValidator = new DateValidator();
        }

        /// <summary>
        /// On click handler File -> ReadInvoice
        /// </summary>
        /// <param name="sender">object sending request</param>
        /// <param name="e">event arguments of request</param>
        private void ReadInvoiceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFildDialog = new OpenFileDialog();
            oFildDialog.Title = "Select invoice to read into the program";
            oFildDialog.InitialDirectory = System.IO.Path.GetFullPath($"{Directory.GetCurrentDirectory()} ..\\..\\InvoiceToFilesToRead");
            oFildDialog.Filter = "Text files (*.txt)|*.txt";
            if (oFildDialog.ShowDialog() == true)
            {
                fileReader = new FileReader(invoiceMapper);
                string[] invoiceFileLines = fileReader.ReadFile(oFildDialog.FileName);
                invoiceMapper = new InvoiceMapper(invoiceFileLines);
                invoiceMapper.AddErrorsIfAnyInFile();

                if (invoiceMapper.InvoiceErrors.Count() == 0)
                {
                    invoice = invoiceMapper.StartToMappInvoice();
                    WriteInvoiceGeneralsToGUI(invoice);
                    PopulateTableOfInvoiceItems(invoice.Products);
                    totalInvoicePrice = invoiceCalculator.CalculatePriceInvoice(invoice.Products);
                }
                else
                {
                    OutPutInvoiceErrors();
                }
            }
        }

        /// <summary>
        /// On click handler File -> Load Company loggo
        /// </summary>
        /// <param name="sender">object sending request</param>
        /// <param name="e">event arguments of request</param>
        private void LoadCompanyLogoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFileDialogImage = new OpenFileDialog();
            oFileDialogImage.Title = "Select company image";
            oFileDialogImage.Filter = "PNG Image (*.png)|*.png|JPG Image (*.jpg)|*.jpg";
            if (oFileDialogImage.ShowDialog() == true)
            {
                BitmapImage imageBitmap = new BitmapImage(new Uri(oFileDialogImage.FileName));
                companyLoggo.Source = imageBitmap;
            }
        }

        /// <summary>
        /// Click handler for File -> Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Write invoice information to the GUI
        /// </summary>
        /// <param name="invoice">invoice data to write</param>
        private void WriteInvoiceGeneralsToGUI(Invoice invoice)
        {
            InvoiceNumberTextBox.Text = invoice.InvoiceNumber;
            InvoiceDateTextBox.Text = invoice.InvoiceDate.ToString("yyyy-MM-dd");
            InvoiceDueDateTextBox.Text = invoice.InvoiceDueDate.ToString("yyyy-MM-dd");
            CompanyInfoOfSellerTextBox.Text = invoice.SellerCompanyInfo.ToString();
            CompanyInfoOfCustomerTextBox.Text = invoice.CustomerCompanyInfo.ToString();
        }

        /// <summary>
        /// Populates the products table with products
        /// </summary>
        /// <param name="products">the products to insert in table</param>
        private void PopulateTableOfInvoiceItems(List<Product> products)
        {
            for (int i = 0; i < products.Count(); i++)
            {
                Product currentProduct = products[i];

                double taxOfProductPrice = invoiceCalculator.CalculateTaxOfProduct(currentProduct.ProductPrercentTax, currentProduct.ProductPrice);
                double priceOfProduct = invoiceCalculator.CalculateTotalPriceOfProduct(taxOfProductPrice, currentProduct.ProductPrice);

                Paragraph productIdParagraph = new Paragraph(new Run(GetIdOfProduct(i)));
                Paragraph productDescriptionParagraph = new Paragraph(new Run(currentProduct.ProductDescription));
                Paragraph productQuantityParagraph = new Paragraph(new Run(currentProduct.ProductQuantity.ToString()));
                Paragraph productPriceParagraph = new Paragraph(new Run(currentProduct.ProductPrice.ToString()));
                Paragraph productPercentTaxParagraph = new Paragraph(new Run(currentProduct.ProductPrercentTax.ToString()));
                Paragraph productTotalTaxParagraph = new Paragraph(new Run(taxOfProductPrice.ToString("N2")));
                Paragraph productTotalPriceParagraph = new Paragraph(new Run(priceOfProduct.ToString("N2")));

                TableCell tableCellId = new TableCell(productIdParagraph);
                TableCell tableCellDescription = new TableCell(productDescriptionParagraph);
                TableCell tableCellQuantity = new TableCell(productQuantityParagraph);
                TableCell tableCellPrice = new TableCell(productPriceParagraph);
                TableCell tableCellPercentTax = new TableCell(productPercentTaxParagraph);
                TableCell tableCellTotalTax = new TableCell(productTotalTaxParagraph);
                TableCell tableCellTotalPrice = new TableCell(productTotalPriceParagraph);

                TableRow tableRow = new TableRow();
                TableCell[] tableCells = new TableCell[] { tableCellId, tableCellDescription, tableCellQuantity, tableCellPrice, tableCellPercentTax, tableCellTotalTax, tableCellTotalPrice };

                AddTableCellsToRow(ApplyBorderToCellsLeftBottom(tableCells), tableRow);

                TableRowGroupItemsOfInvoice.Rows.Add(tableRow);
            }

            AddTotalsDiscountFieldToTableOfInvoiceItems(invoiceCalculator.CalculateTaxInvoice(invoice.Products), invoiceCalculator.CalculatePriceInvoice(invoice.Products));
        }

        /// <summary>
        /// Adds fields for showing total tax, total price, as well as a field for discount to the products table
        /// </summary>
        /// <param name="taxTotal">total tax of invoice</param>
        /// <param name="priceTotal">total price of invoice</param>
        private void AddTotalsDiscountFieldToTableOfInvoiceItems(double taxTotal, double priceTotal)
        {

            TableRow taxTotalRow = new TableRow();
            TableRow priceTotalRow = new TableRow();
            TableRow discountRow = new TableRow();

            ApplyBorderToCellsBottom(CrateTaxTotalTableCells(taxTotal));
            ApplyBorderToCellsBottom(CreatePriceTotalTableCell(priceTotal));
            ApplyBorderToCellsBottom(CreateDiscountFieldInTableCells());

            AddTableCellsToRow(CrateTaxTotalTableCells(taxTotal), taxTotalRow);
            AddTableCellsToRow(CreatePriceTotalTableCell(priceTotal), priceTotalRow);
            AddTableCellsToRow(CreateDiscountFieldInTableCells(), discountRow);

            TableRow[] tableRowsToAdd = new TableRow[] { taxTotalRow, priceTotalRow, discountRow };

            AddRowsToTableRowGroup(tableRowsToAdd, TableRowGroupItemsOfInvoice);
        }

        /// <summary>
        /// Adds rows to the product tables tableRowGroup
        /// </summary>
        /// <param name="tableRows">tableRows to add</param>
        /// <param name="tableRowGroup">the tableRowGroup receiving the rows</param>
        private void AddRowsToTableRowGroup(TableRow[] tableRows, TableRowGroup tableRowGroup)
        {
            foreach (TableRow tableRow in tableRows)
            {
                tableRowGroup.Rows.Add(tableRow);
            }
        }

        /// <summary>
        /// Creates the Cell to holt the total tax
        /// </summary>
        /// <param name="taxTotal">total tax to show</param>
        /// <returns></returns>
        private TableCell[] CrateTaxTotalTableCells(double taxTotal)
        {
            Paragraph taxTotalParagraph = new Paragraph(new Run($"Total tax: {taxTotal.ToString("N2")}"));

            TableCell taxTotalTableCell = new TableCell(taxTotalParagraph);
            taxTotalTableCell.ColumnSpan = 6;

            return new TableCell[] { taxTotalTableCell };
        }

        /// <summary>
        /// Creates the discount field table cells
        /// </summary>
        /// <returns></returns>
        private TableCell[] CreateDiscountFieldInTableCells()
        {
            Paragraph priceDiscountParagraph = new Paragraph(new Run("Add Discount"));
            TextBox priceDiscountTextBox = new TextBox();
            priceDiscountTextBox.ToolTip = "Please press enter to change";
            priceDiscountTextBox.Name = "PriceDiscountTextBox";
            priceDiscountTextBox.KeyUp += new KeyEventHandler(PriceDiscountTextBox_KeyUp);
            priceDiscountTextBox.BorderThickness = new Thickness(0, 0, 0, 1);
            priceDiscountTextBox.BorderBrush = Brushes.Gray;
            BlockUIContainer blockUiContainerDiscount = new BlockUIContainer(priceDiscountTextBox);

            TableCell priceDiscountParagraphTableCell = new TableCell(priceDiscountParagraph);
            priceDiscountParagraphTableCell.ColumnSpan = 2;
            TableCell priceDiscountTextBoxTableCell = new TableCell(blockUiContainerDiscount);

            return new TableCell[] { priceDiscountParagraphTableCell, priceDiscountTextBoxTableCell };
        }

        /// <summary>
        /// Creates the table cell holding the total price
        /// </summary>
        /// <param name="priceTotal">total price to show</param>
        /// <returns></returns>
        private TableCell[] CreatePriceTotalTableCell(double priceTotal)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            Paragraph priceTotalLabelParagraph = new Paragraph(new Run($"Total price: {priceTotal.ToString("N2")}"));

            TableCell priceTotalTableCell = new TableCell(priceTotalLabelParagraph);
            priceTotalTableCell.ColumnSpan = 6;

            priceTotalTableCell.BorderThickness = new Thickness(0, 0, 0, 1);
            priceTotalTableCell.BorderBrush = Brushes.Gray;

            return new TableCell[] { priceTotalTableCell };
        }
        
        /// <summary>
        /// Get id of product
        /// </summary>
        /// <param name="i">product index, from whichToCalCulate the id</param>
        /// <returns></returns>
        private static string GetIdOfProduct(int i)
        {
            return (i + 1).ToString();
        }

        /// <summary>
        /// Applies border to Left and bottom around tableCells
        /// </summary>
        /// <param name="tableCells">table cells to add borders to</param>
        /// <returns></returns>
        private TableCell[] ApplyBorderToCellsLeftBottom(TableCell[] tableCells)
        {
            for (int i = 0; i < tableCells.Length; i++)
            {
                if (i == 0)
                {
                    tableCells[i].BorderThickness = new Thickness(0, 0, 0, 1);
                    tableCells[i].BorderBrush = Brushes.Gray;
                }
                else
                {
                    tableCells[i].BorderThickness = new Thickness(1, 0, 0, 1);
                    tableCells[i].BorderBrush = Brushes.Gray;
                }
            }

            return tableCells;
        }

        /// <summary>
        /// Applies border to bottom around tableCells
        /// </summary>
        /// <param name="tableCells">table cells to add border to</param>
        /// <returns></returns>
        private TableCell[] ApplyBorderToCellsBottom(TableCell[] tableCells)
        {
            for (int i = 0; i < tableCells.Length; i++)
            {
                if (i == (tableCells.Length - 1))
                {
                    tableCells[i].BorderThickness = new Thickness(0);
                    tableCells[i].BorderBrush = Brushes.Gray;
                }
                else
                {
                    tableCells[i].BorderThickness = new Thickness(0, 0, 0, 1);
                    tableCells[i].BorderBrush = Brushes.Gray;
                }
            }

            return tableCells;
        }

        /// <summary>
        /// Adds table cells to a row
        /// </summary>
        /// <param name="tableCells">tableCells to add to the row</param>
        /// <param name="tableRow">the row to add cells to</param>
        private void AddTableCellsToRow(TableCell[] tableCells, TableRow tableRow)
        {
            foreach (TableCell cell in tableCells)
            {
                tableRow.Cells.Add(cell);
            }
        }

        /// <summary>
        /// On KeyUp handler for pressing a key in the invoiceDateTextBox, initiate check date on enter 
        /// </summary>
        /// <param name="sender">object sending request</param>
        /// <param name="e">event arguments of request</param>
        private void InvoiceDateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!dateValidator.IsDateValid(InvoiceDateTextBox.Text))
                {
                    MessageBoxUtility.ShowErrorMessageBox(ErrorMessages.dateError);
                }
            }
        }

        /// <summary>
        /// On KeyUp handler for pressing a key in the invoiceDueDateTextBox, initiate check date on enter 
        /// </summary>
        /// <param name="sender">object sending request</param>
        /// <param name="e">event arguments of request</param>
        private void InvoiceDueDateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!dateValidator.IsDateValid(InvoiceDateTextBox.Text))
                {
                    MessageBoxUtility.ShowErrorMessageBox(ErrorMessages.dateError);
                }
            }
        }

        /// <summary>
        /// Outut error collectedDuring error check of invoice data
        /// </summary>
        private void OutPutInvoiceErrors()
        {
            MessageBoxUtility.ShowErrorMessageBox(string.Join("\n\n", invoiceMapper.InvoiceErrors), invoiceMapper.InvoiceErrors.Count());
        }

        /// <summary>
        /// On KeyUp handler for pressing a key in the priceDiscountTextBox, initiate check number on enter 
        /// </summary>
        /// <param name="sender">object sending request</param>
        /// <param name="e">event arguments of request</param>
        private void PriceDiscountTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox discountTetBox = (TextBox)sender;
                string inputString = discountTetBox.Text;
                int decimalIndex = numberValidator.AddSeparatorIfSeparatorNotExists(ref inputString);

                bool numberOk = numberValidator.IsNumberValid(inputString, decimalIndex, out double result);
                if (numberOk)
                {
                    UpdatePrice(result);
                    discountTetBox.Text = result.ToString("N2");
                }
                else
                {
                    MessageBoxUtility.ShowErrorMessageBox(ErrorMessages.numberError);
                }
            }
        }

        /// <summary>
        /// Updates the initial total price with the discount, if discount is not to big
        /// </summary>
        /// <param name="discount">discount to withdraw from the total price</param>
        private void UpdatePrice(double discount)
        {
            if (discount <= totalInvoicePrice)
            {
                int numberRows = TableRowGroupItemsOfInvoice.Rows.Count();
                UpdateTotalTaxAndPriceParagraph(discount, numberRows);
            }
            else
            {
                MessageBoxUtility.ShowErrorMessageBox("The discount can´t be greater than the price");
            }
        }

        /// <summary>
        /// Updates the paragraphs in the table cells containing the total price and totalTax
        /// </summary>
        /// <param name="discount">the discount</param>
        /// <param name="numberRows">numberOfRows in product table</param>
        private void UpdateTotalTaxAndPriceParagraph(double discount, int numberRows)
        {
            TableCell cellTotalPrice = TableRowGroupItemsOfInvoice.Rows[numberRows - 2].Cells[0];
            Paragraph totalPriceParagraph = (Paragraph)cellTotalPrice.Blocks.ElementAt(0);

            double formerPrice = invoiceCalculator.CalculatePriceInvoice(invoice.Products);
            double priceAfterDiscount = formerPrice - discount;

            totalPriceParagraph.Inlines.Clear();
            totalPriceParagraph.Inlines.Add($"Total price after ({discount}) withdrawn: {priceAfterDiscount.ToString("N2")}");

            TableCell cellTotalTax = TableRowGroupItemsOfInvoice.Rows[numberRows - 3].Cells[0];
            Paragraph totalTaxParagraph = (Paragraph)cellTotalTax.Blocks.ElementAt(0);

            double taxAfterDiscount = priceAfterDiscount * 0.25;

            totalTaxParagraph.Inlines.Clear();
            totalTaxParagraph.Inlines.Add($"Total tax after ({discount}) withdrawn: {taxAfterDiscount.ToString("N2")}");
        }
    }
}
