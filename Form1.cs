using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChooseAlternatives
{
    public partial class Form1 : Form
    {

        Int32 numOfColumns = 0;
        Int32 numOfRows = 0;
        Int32 j = 1;
        int r = 0;
        int a = 0;

        List<string> _names = new List<string>();

        List<double[]> _dataArray = new List<double[]>();

        DataTable d = new DataTable();
        DataTable d1 = new DataTable();

        int max = 0;
        int num = 0;
        int min = 0;

        int max1 = 0;
        int num1 = 0;
        int min1 = 0;

        List<int> _maxList= new List<int>();
        List<int> _minList = new List<int>();

        string smax;
        string snum;
        string smin;

        public Form1()
        {
            InitializeComponent();

        }

        private void btnGen_Click(object sender, EventArgs e)
        {


            numOfColumns = Decimal.ToInt32(numColumn.Value);
            numOfRows = Decimal.ToInt32(numRows.Value);


            for (j = 1; j <= numOfColumns; j++)
            {

                _names.Add("s" + j);
                _dataArray.Add(new Double[numOfRows]);

            }


            for (int i = 0; i < this._dataArray.Count; i++)
            {
                // The current process name.
                string name = this._names[i];

                // Add the program name to our columns.
                d.Columns.Add(name);

                // Add all of the memory numbers to an object list.
                List<object> objectNumbers = new List<object>();

                //Put every column's numbers in this List.
                foreach (double number in this._dataArray[i])
                {
                    objectNumbers.Add((object)number);
                }

                // Keep adding rows until we have enough.
                while (d.Rows.Count < objectNumbers.Count)
                {
                    d.Rows.Add();
                }

                dgv.DataSource = d;

            }


            d1.Columns.Add();

            for (int t = 0; t < numOfRows; t++)
            {
                d1.Rows.Add();
                d1.Rows[t][0] = "A" + (t + 1);
            }

            dgv1.DataSource = d1;

            pnlTable.Visible = true;


            btnGen.Enabled = false;
            btnClr.Enabled = true;

            btnOptimistic.Visible = true;
            btnPsmstc.Visible = true;
            btnReg.Visible = true;

           //btnOptimistic.Enabled = true;
           //btnPsmstc.Enabled = true;


            cmbProfitCost.Enabled = false;

            numColumn.Enabled = false;
            numRows.Enabled = false;
            
            lblAppro.Visible = true;
            //lblWarn.Visible = true;
            //lblSeq.Visible = true;

        }

        private void btnClr_Click(object sender, EventArgs e)
        {


            numOfColumns = 0;
            numOfRows = 0;
            j = 1;
            r = 0;
            a = 0;
            

            _names = new List<string>();

            _dataArray = new List<double[]>();

            _maxList = new List<int>();
            _minList = new List<int>();


            max = 0;
            num = 0;
            min = 0;

            max1 = 0;
            num1 = 0;
            min1 = 0;
           

            d = new DataTable();
            d1 = new DataTable();

            pnlTable.Visible = false;

            btnGen.Enabled = true;
            btnClr.Enabled = false;
            btnOptimistic.Visible = false;
            btnPsmstc.Visible = false;
            btnReg.Visible = false;
            btnOptimistic.Enabled = true;
            btnPsmstc.Enabled = false;
            btnReg.Enabled = false;


            cmbProfitCost.Enabled = true;

            numColumn.Enabled = true;
            numRows.Enabled = true;

            lblOpt.Visible = false;
            lblOpt1.Visible = false;

            lblPsmstc.Visible = false;
            lblPsmstc1.Visible = false;

            lblReg.Visible = false;
            lblReg1.Visible = false;

            lblAppro.Visible = false;
            //lblWarn.Visible = false;
            //lblSeq.Visible = false;
        }


        private void btnOptimistic_Click(object sender, EventArgs e)
        {

            bool Empty = true;

            for (r = 0; r < numOfRows; r++)
            {
                for (int c = 0; c < numOfColumns; c++)
                {
                    if (d.Rows[r][c] != null && d.Rows[r][c].ToString().Trim() != "")
                    {
                        Empty = false;
                    }
                    else
                    {
                        Empty = true;
                        break;
                    }
                }

            }

            if (Empty == true)
            {
                MessageBox.Show("PLEASE, DON'T LEAVE EMPTY CELLS !", "ERROR , Emtpy Cells !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Empty == false)
            {
                _maxList = new List<int>();
                _minList = new List<int>();

                r = 0;

                max = 0;
                num = 0;
                min = 0;

                max1 = 0;
                num1 = 0;
                min1 = 0;


                if (cmbProfitCost.SelectedIndex == 0)
                {

                    for (r = 0; r < numOfRows; r++)
                    {

                        smax = Convert.ToString(d.Rows[r]["S1"]);
                        max = int.Parse(smax);

                        for (int c = 2; c <= numOfColumns; c++)
                        {

                            snum = Convert.ToString(d.Rows[r]["s" + c]);

                            num = int.Parse(snum);


                            if (num >= max)
                            {
                                max = num;
                            }

                        }

                        _maxList.Add(max);
                    }


                    max1 = _maxList[0];

                    for (int i = 1; i < _maxList.Count; i++)
                    {

                        num1 = _maxList[i];

                        if (num1 >= max1)
                        {
                            max1 = num1;
                        }

                    }

                    a = _maxList.IndexOf(max1);


                    lblOpt.Text = "Highest Profit is " + max1.ToString();
                    lblOpt1.Text = "Best Alternative is " + Convert.ToString(d1.Rows[a][0]);


                }


                else if (cmbProfitCost.SelectedIndex == 1)
                {
                    for (r = 0; r < numOfRows; r++)
                    {

                        smin = Convert.ToString(d.Rows[r]["S1"]);

                        min = int.Parse(smin);

                        for (int c = 2; c <= numOfColumns; c++)
                        {

                            snum = Convert.ToString(d.Rows[r]["s" + c]);

                            num = int.Parse(snum);


                            if (num <= min)
                            {
                                min = num;
                            }

                        }

                        _minList.Add(min);
                    }

                    min1 = _minList[0];

                    for (int i = 1; i < _minList.Count; i++)
                    {

                        num1 = _minList[i];

                        if (num1 <= min1)
                        {
                            min1 = num1;
                        }

                    }

                    a = _minList.IndexOf(min1);


                    lblOpt.Text = "Least Cost is " + min1.ToString();
                    lblOpt1.Text = "Best Alternative is " + Convert.ToString(d1.Rows[a][0]);

                }

                lblOpt.Visible = true;
                lblOpt1.Visible = true;


                btnOptimistic.Enabled = false;
                btnPsmstc.Enabled = true;

            }
        }

        private void btnPsmstc_Click(object sender, EventArgs e)
        {

            _maxList = new List<int>();
            _minList = new List<int>();

            r = 0;

            max = 0;
            num = 0;
            min = 0;

            max1 = 0;
            num1 = 0;
            min1 = 0;

            if (cmbProfitCost.SelectedIndex == 0)
            {

                for (r = 0; r < numOfRows; r++)
                {

                    smin = Convert.ToString(d.Rows[r]["S1"]);

                    min = int.Parse(smin);

                    for (int c = 2; c <= numOfColumns; c++)
                    {

                        snum = Convert.ToString(d.Rows[r]["s" + c]);

                        num = int.Parse(snum);


                        if (num <= min)
                        {
                            min = num;
                        }

                    }

                    _minList.Add(min);
                }


                max1 = _minList[0];

                for (int i = 1; i < _minList.Count; i++)
                {

                    num1 = _minList[i];

                    if (num1 >= max1)
                    {
                        max1 = num1;
                    }

                }

                a = _minList.IndexOf(max1);


                lblPsmstc.Text = "Highest Profit is " + max1.ToString();
                lblPsmstc1.Text = "Best Alternative is " + Convert.ToString(d1.Rows[a][0]);


            }

            else if (cmbProfitCost.SelectedIndex == 1)
            {
                for (r = 0; r < numOfRows; r++)
                {

                    smax = Convert.ToString(d.Rows[r]["S1"]);
                    max = int.Parse(smax);

                    for (int c = 2; c <= numOfColumns; c++)
                    {

                        snum = Convert.ToString(d.Rows[r]["s" + c]);

                        num = int.Parse(snum);


                        if (num >= max)
                        {
                            max = num;
                        }

                    }

                    _maxList.Add(max);
                }



                min1 = _maxList[0];

                for (int i = 1; i < _maxList.Count; i++)
                {

                    num1 = _maxList[i];

                    if (num1 <= min1)
                    {
                        min1 = num1;
                    }

                }

                a = _maxList.IndexOf(min1);


                lblPsmstc.Text = "Least Cost is " + min1.ToString();
                lblPsmstc1.Text = "Best Alternative is " + Convert.ToString(d1.Rows[a][0]);


            }


            lblPsmstc.Visible = true;
            lblPsmstc1.Visible = true;

            btnPsmstc.Enabled = false;
            btnReg.Enabled = true;

        }


        private void btnReg_Click(object sender, EventArgs e)
        {

            _maxList = new List<int>();
            _minList = new List<int>();

            r = 0;

            max = 0;
            num = 0;
            min = 0;

            max1 = 0;
            num1 = 0;
            min1 = 0;


            if (cmbProfitCost.SelectedIndex == 0)
            {

                for (int c = 1; c <= numOfColumns; c++)
                {

                    smax = Convert.ToString(d.Rows[0]["S" + c]);
                    max = int.Parse(smax);

                    for (r = 1; r < numOfRows; r++)
                    {
                        snum = Convert.ToString(d.Rows[r]["s" + c]);

                        num = int.Parse(snum);

                        if (num >= max)
                        {
                            max = num;
                        }
                    }


                    for (r = 0; r < numOfRows; r++)
                    {

                        d.Rows[r]["s" + c] = max - int.Parse(d.Rows[r]["s" + c].ToString());

                    }
                }


                for (r = 0; r < numOfRows; r++)
                {

                    smax = Convert.ToString(d.Rows[r]["S1"]);
                    max = int.Parse(smax);

                    for (int c = 2; c <= numOfColumns; c++)
                    {

                        snum = Convert.ToString(d.Rows[r]["s" + c]);

                        num = int.Parse(snum);


                        if (num >= max)
                        {
                            max = num;
                        }

                    }

                    _maxList.Add(max);
                }


                min1 = _maxList[0];

                for (int i = 1; i < _maxList.Count; i++)
                {

                    num1 = _maxList[i];

                    if (num1 <= min1)
                    {
                        min1 = num1;
                    }

                }

                a = _maxList.IndexOf(min1);


                lblReg.Text = "Highest Profit is " + min1.ToString();
                lblReg1.Text = "Best Alternative is " + Convert.ToString(d1.Rows[a][0]);

            }

            else if (cmbProfitCost.SelectedIndex == 1)
            {

                for (int c = 1; c <= numOfColumns; c++)
                {

                    smin = Convert.ToString(d.Rows[0]["S" + c]);
                    min = int.Parse(smin);

                    for (r = 1; r < numOfRows; r++)
                    {
                        snum = Convert.ToString(d.Rows[r]["s" + c]);

                        num = int.Parse(snum);

                        if (num <= min)
                        {
                            min = num;
                        }
                    }


                    for (r = 0; r < numOfRows; r++)
                    {

                        d.Rows[r]["s" + c] = int.Parse(d.Rows[r]["s" + c].ToString()) - min ;

                    }

                }

                    for (r = 0; r < numOfRows; r++)
                    {

                        smax = Convert.ToString(d.Rows[r]["S1"]);
                        max = int.Parse(smax);

                        for (int c = 2; c <= numOfColumns; c++)
                        {

                            snum = Convert.ToString(d.Rows[r]["s" + c]);

                            num = int.Parse(snum);


                            if (num >= max)
                            {
                                max = num;
                            }

                        }

                        _maxList.Add(max);
                    }


                    min1 = _maxList[0];

                    for (int i = 1; i < _maxList.Count; i++)
                    {

                        num1 = _maxList[i];

                        if (num1 <= min1)
                        {
                            min1 = num1;
                        }

                    }

                    a = _maxList.IndexOf(min1);


                    lblReg.Text = "Least Cost is " + min1.ToString();
                    lblReg1.Text = "Best Alternative is " + Convert.ToString(d1.Rows[a][0]);
         
            }


            lblReg.Visible = true;
            lblReg1.Visible = true;


            btnReg.Enabled = false;


            }

        private void cmbProfitCost_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnGen.Enabled = true;

        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            int RowIndex = e.RowIndex;

            int columnIndex = e.ColumnIndex;

          for (int i=0; i<numOfColumns;i++)
          {

              bool validation = true;

              if (e.ColumnIndex == i)
              {

               if (dgv.Rows[RowIndex].Cells[columnIndex].Value != null && dgv.Rows[RowIndex].Cells[columnIndex].Value.ToString().Trim() != "")
                  {

                      string DataToValidate = dgv.Rows[RowIndex].Cells[columnIndex].Value.ToString();

                      foreach (char c in DataToValidate)
                      {

                          if (!char.IsDigit(c))
                          {

                              validation = false;

                              break;

                          }

                       //   else if (char.IsSymbol(c)) 
                         // {
                           //   validation = false;

                             // break;
                          //}

                      }

                      if (validation == false)
                      {

                          //dgv.Rows[RowIndex].Cells[columnIndex].ErrorText = "Please Enter an 'Integer'!";

                          MessageBox.Show("PLEASE, ENTER NUMERS ONLY !", "ERROR , Invalid Value !", MessageBoxButtons.OK, MessageBoxIcon.Error);

                          dgv.Rows[RowIndex].Cells[columnIndex].Value = "";

                      }

                  }

              }

        }

          if (dgv.Rows[(numOfRows - 1)].Cells[(numOfColumns - 1)].Value != null && dgv.Rows[(numOfRows - 1)].Cells[(numOfColumns - 1)].Value.ToString().Trim() != "")
          {

             // btnOptimistic.Enabled = true;

          }

     }

        private void lblOpt_Click(object sender, EventArgs e)
        {
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv_MouseClick(object sender, MouseEventArgs e)
        {          
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dgv_CellErrorTextNeeded(object sender, DataGridViewCellErrorTextNeededEventArgs e)
        {

            

        }

        private void dgv_CellErrorTextChanged(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void pnlCIE_Paint(object sender, PaintEventArgs e)
        {

        }

      }

    }


