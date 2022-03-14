using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

//ImageProcessing Application Student Version
//Created by Mr. Iannotta for ICS4U

namespace ImageProcessing
{
    public partial class frmMain : Form
    {
        private Color[,] original; //this is the original picture - never change the values stored in this array
        private Color[,] transformedPic;  //transformed picture that is displayed   
        public frmMain()
        {
            InitializeComponent();
        }
       
        protected override void OnPaint(PaintEventArgs e)
        {
            //this method draws the transformed picture
            //what ever is stored in transformedPic array will
            //be displayed on the form

            base.OnPaint(e);

            Graphics g = e.Graphics;

            //only draw if picture is transformed
            if (transformedPic != null)
            {
                //get height and width of the transfrormedPic array
                int height = transformedPic.GetUpperBound(0)+1;
                int width = transformedPic.GetUpperBound(1) + 1;

                //create a new Bitmap to be dispalyed on the form
                Bitmap newBmp = new Bitmap(width, height);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        //loop through each element transformedPic and set the 
                        //colour of each pixel in the bitmalp
                        newBmp.SetPixel(j, i, transformedPic[i, j]);
                    }

                }
                //call DrawImage to draw the bitmap
                g.DrawImage(newBmp, 0, 20, width, height);
            }
            
        }


        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            //this method reads in a picture file and stores it in an array

            //try catch should handle any errors for invalid picture files
            try
            {

                //open the file dialog to select a picture file

                OpenFileDialog fd = new OpenFileDialog();

                //create a bitmap to store the file in
                Bitmap bmp;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    //store the selected file into a bitmap
                    bmp = new Bitmap(fd.FileName);

                    //create the arrays that store the colours for the image
                    //the size of the arrays is based on the height and width of the bitmap
                    //initially both the original and transformedPic arrays will be identical
                    original = new Color[bmp.Height, bmp.Width];
                    transformedPic = new Color[bmp.Height, bmp.Width];
                    

                    //load each color into a color array
                    for (int i = 0; i < bmp.Height; i++)//each row
                    {
                        for (int j = 0; j < bmp.Width; j++)//each column
                        {
                            //assign the colour in the bitmap to the array
                            original[i, j] = bmp.GetPixel(j, i);
                            transformedPic[i, j] = original[i, j];
                        }
                    }
                    //this will cause the form to be redrawn and OnPaint() will be called
                    this.Refresh();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Picture File. \n" + ex.Message);
            }
            
        }

        private void mnuProcessDarken_Click(object sender, EventArgs e)
        {
            //code to Darken
            if (transformedPic!= null)
            {
                int Red, Green, Blue;
                for (int i =0; i < transformedPic.GetLength(0);i++)
                {
                    for(int j=0; j < transformedPic.GetLength(1);j++)
                    {
                        Red = transformedPic[i, j].R-10;
                        if (Red < 0) 
                            Red = 0;
                        Green = transformedPic[i, j].G - 10;
                        if (Green < 0)
                            Green = 0;
                        Blue = transformedPic[i, j].B - 10;
                        if (Blue < 0)
                            Blue = 0;

                        transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                    }
                }    


            }




            this.Refresh();
        }

        private void mnuProcessInvert_Click(object sender, EventArgs e)
        {
            //code to Invert
            
            if (transformedPic != null)
            {
                int Red, Green, Blue;
                for (int i = 0; i < transformedPic.GetLength(0); i++)
                {
                    for (int j = 0; j < transformedPic.GetLength(1); j++)
                    {
                        Red = 255 - transformedPic[i, j].R ;
                        if (Red < 0)
                            Red = 0;
                        Green = 255- transformedPic[i, j].G ;
                        if (Green < 0)
                            Green = 0;
                        Blue = 255 - transformedPic[i, j].B ;
                        if (Blue < 0)
                            Blue = 0;

                        transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                    }
                }
                

            }

            this.Refresh();
        }

        private void mnuProcessWhiten_Click(object sender, EventArgs e)
        {
            //code to Whiten
            // cap is 255 
            // add 10
           


            if (transformedPic != null)
            {
                int Red, Green, Blue;
                for (int i = 0; i < transformedPic.GetLength(0); i++)
                {
                    for (int j = 0; j < transformedPic.GetLength(1); j++)
                    {
                        Red = transformedPic[i, j].R + 10;
                        if (Red  >255)
                            Red = 255;
                        Green = transformedPic[i, j].G + 10;
                        if (Green > 255)
                            Green = 255;
                        Blue = transformedPic[i, j].B + 10;
                        if (Blue > 255)
                            Blue = 255;

                        transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                    }
                   
                }
                this.Refresh();
            }
        }
        private void mnuProcessRotate_Click(object sender, EventArgs e)
        {
            //code to Rotate

            if (transformedPic != null)
            {
                int Rows = transformedPic.GetLength(0);
                int Columns = transformedPic.GetLength(1);
                Color[,] TempRotate= new Color[Rows, Columns];
                // Copy into temp array
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        TempRotate[i, j] = transformedPic[i, j];

                    }
                }
                transformedPic = new Color[Columns, Rows];
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {

                        transformedPic[j, Rows -1-i] = TempRotate[i, j];
                        
                    }

                }
                this.Refresh();

            }

            this.Refresh();
        }

        private void mnuProcessReset_Click(object sender, EventArgs e)
        {
            //code to Reset
           
           
            if (transformedPic !=null)
            {
                int Rows = original.GetLength(0);
                int Columns = original.GetLength(1);
                transformedPic = new Color[Rows, Columns];

                for (int i = 0; i < transformedPic.GetLength(0); i++)
                {
                    for (int j = 0; j < transformedPic.GetLength(1); j++)
                    {
                        transformedPic[i, j] = original[i, j];

                    }
                }
                this.Refresh();
            }
         

        }

        private void mnuProcessFlipX_Click(object sender, EventArgs e)
        {
            //code to FlipX
            
            Color Pixel;
            if (transformedPic != null)
            {


                for (int i = 0; i < transformedPic.GetLength(0); i++)
                {
                    for (int j = 0; j < transformedPic.GetLength(1) / 2; j++)
                    {
                        Pixel = transformedPic[i, j];
                        transformedPic[i, j] = transformedPic[i, transformedPic.GetLength(1) - 1 - j];
                        transformedPic[i, transformedPic.GetLength(1) - 1 - j] = Pixel;

                    }
                }
                this.Refresh();
            }

        }

        private void mnuProcessFlipY_Click(object sender, EventArgs e)
        {
            //code to FlipY
            
            if (transformedPic != null)
            {
                Color Pixel;
                for (int i = 0; i < transformedPic.GetLength(0) / 2; i++)
                {
                    for (int j = 0; j < transformedPic.GetLength(1); j++)
                    {
                        Pixel = transformedPic[i, j];
                        transformedPic[i, j] = transformedPic[transformedPic.GetLength(0) - 1 - i, j];
                        transformedPic[transformedPic.GetLength(0) - 1 - i, j] = Pixel;

                    }

                }
                this.Refresh();
            }


        }

        private void mnuProcessMirrorH_Click(object sender, EventArgs e)
        {
            //code to Mirror Horizontally
            
           

            if (transformedPic != null)
            {
                int Rows = transformedPic.GetLength(0);
                int Columns = transformedPic.GetLength(1);
                Color [,] TempHorizontal = new Color[Rows, Columns];
                // Copy into temp array
                for (int i = 0; i <Rows ; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        TempHorizontal[i, j] = transformedPic[i, j];

                    }
                }
                
               
                    transformedPic = new Color[Rows, Columns*2];
                  

                
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {

                        transformedPic[i, j] = TempHorizontal[i, j];
                        transformedPic[i, Columns * 2 - 1 - j] = TempHorizontal[i, j];
                    }

                }
                this.Refresh();

            }
        }

        private void mnuProcessMirrorV_Click(object sender, EventArgs e)
        {
            //code to Mirror Vertically
            

            if (transformedPic != null)
            {
                int Rows = transformedPic.GetLength(0);
                int Columns = transformedPic.GetLength(1);
                Color[,] TempVertical = new Color[Rows, Columns];
                // Copy into temp array
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        TempVertical[i, j] = transformedPic[i, j];

                    }
                }
            
                    transformedPic = new Color[Rows * 2, Columns];


                
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {

                        transformedPic[i, j] = TempVertical[i, j];
                        transformedPic[Rows*2-1-i, j] = TempVertical[i, j];
                    }

                }
                this.Refresh();

            }
        }

        private void mnuProcessScale50_Click(object sender, EventArgs e)
        {
            //code to Scale 50%


            if (transformedPic != null)
            {
                int Rows = transformedPic.GetLength(0);
                int Columns = transformedPic.GetLength(1);
                Color[,] Temp = new Color[Rows, Columns];
                // Copy into temp array
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        Temp[i, j] = transformedPic[i, j];

                    }
                }
                transformedPic = new Color[Rows/2, Columns/2];
                for (int i = 0; i < Rows/2; i++)
                {
                    for (int j = 0; j < Columns/2; j++)
                    {

                        transformedPic[i, j] = Temp[i*2, j*2];

                    }

                }

            }
                    this.Refresh();
        }

        private void mnuProcessScale200_Click(object sender, EventArgs e)
        {
            //code to Scale 200%

            if (transformedPic != null)
            {
                int Rows = transformedPic.GetLength(0);
                int Columns = transformedPic.GetLength(1);
                Color[,] Temp = new Color[Rows, Columns];
                // Copy into temp array
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        Temp[i, j] = transformedPic[i, j];

                    }
                }
                transformedPic = new Color[Rows *2, Columns *2];
                for (int i = 0; i < Rows *2; i++)
                {
                    for (int j = 0; j < Columns *2; j++)
                    {

                        transformedPic[i, j] = Temp[i / 2, j / 2];

                    }

                }

            }
            this.Refresh();
        }

        private void mnuProcessBlur_Click(object sender, EventArgs e)
        {
            //code to Blur
            if (transformedPic != null)
            {
                int Red, Green, Blue;
                int Rows = transformedPic.GetLength(0);
                int Columns = transformedPic.GetLength(1);
                Color[,] Temp = new Color[Rows, Columns];


                // Copy into temp array
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        Temp[i, j] = transformedPic[i, j];
                    }
                }


                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        // First corner piece

                        if (i == 0 && j == 0)
                        {
                            Red = (Temp[i, j].R + Temp[i, j + 1].R + Temp[i + 1, j + 1].R + Temp[i + 1, j].R) / 4;
                            Green = (Temp[i, j].G + Temp[i, j + 1].G + Temp[i + 1, j + 1].G + Temp[i + 1, j].G) / 4;
                            Blue = (Temp[i, j].B + Temp[i, j + 1].B + Temp[i + 1, j + 1].B + Temp[i + 1, j].B) / 4;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                        }
                        //second corner
                        else if (i == 0 && j == Columns - 1)
                        {
                            Red = (Temp[i, j].R + Temp[i, j-1].R + Temp[i + 1, j - 1].R + Temp[i + 1, j].R) / 4;
                            Green = (Temp[i, j].G + Temp[i, j - 1].G + Temp[i + 1, j - 1].G + Temp[i + 1, j].G) / 4;
                            Blue = (Temp[i, j].B + Temp[i, j - 1].B + Temp[i + 1, j - 1].B + Temp[i + 1, j].B) / 4;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);


                        }
                        //third corner

                        else if (i == Rows - 1 && j == 0)
                        {
                            Red = (Temp[i, j].R + Temp[i - 1, j].R + Temp[i - 1, j + 1].R + Temp[i, j + 1].R) / 4;
                            Green = (Temp[i, j].G + Temp[i - 1, j].G + Temp[i - 1, j + 1].G + Temp[i, j + 1].G) / 4;
                            Blue = (Temp[i, j].B + Temp[i - 1, j].B + Temp[i - 1, j + 1].B + Temp[i, j + 1].B) / 4;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                        }
                        //fourth corner
                        else if (i == Rows - 1 && j == Columns - 1)
                        {
                            Red = (Temp[i - 1, j].R + Temp[i, j - 1].R + Temp[i, j].R + Temp[i - 1, j - 1].R) / 4;
                            Green = (Temp[i - 1, j].G + Temp[i, j - 1].G + Temp[i, j].G + Temp[i - 1, j - 1].G) / 4;
                            Blue = (Temp[i - 1, j].B + Temp[i, j - 1].B + Temp[i, j].B + Temp[i - 1, j - 1].B) / 4;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                        }
                        //first column 
                        else if (j == 0)
                        {
                            Red = (Temp[i, j].R + Temp[i - 1, j].R + Temp[i - 1, j + 1].R + Temp[i, j + 1].R + Temp[i + 1, j + 1].R + Temp[i + 1, j].R) / 6;
                            Green = (Temp[i, j].R + Temp[i - 1, j].G + Temp[i - 1, j + 1].G + Temp[i, j + 1].G + Temp[i + 1, j + 1].G + Temp[i + 1, j].G) / 6;
                            Blue = (Temp[i, j].R + Temp[i - 1, j].B + Temp[i - 1, j + 1].B + Temp[i, j + 1].B + Temp[i + 1, j + 1].B + Temp[i + 1, j].B) / 6;

                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);


                        }

                        //first row (top)
                        else if (i == 0)
                        {
                            Red = (Temp[i, j].R + Temp[i, j - 1].R + Temp[i + 1, j - 1].R + Temp[i + 1, j].R + Temp[i + 1, j + 1].R + Temp[i, j + 1].R) / 6;
                            Blue = (Temp[i, j].B + Temp[i, j - 1].B + Temp[i + 1, j - 1].B + Temp[i + 1, j].B + Temp[i + 1, j + 1].B + Temp[i, j + 1].B) / 6;
                            Green = (Temp[i, j].G + Temp[i, j - 1].G + Temp[i + 1, j - 1].G + Temp[i + 1, j].G + Temp[i + 1, j + 1].G + Temp[i, j + 1].G) / 6;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                        }

                        //last row (bottom)
                        else if (i==Rows-1)
                        {
                            Red = (Temp[i, j].R + Temp[i, j -1].R + Temp[i - 1, j-1 ].R + Temp[i -1, j].R + Temp[i - 1, j + 1].R + Temp[i, j + 1].R) / 6;
                            Green = (Temp[i, j].G + Temp[i, j - 1].G + Temp[i - 1, j - 1].G + Temp[i - 1, j].G + Temp[i - 1, j + 1].G + Temp[i, j + 1].G) / 6;
                            Blue = (Temp[i, j].B + Temp[i, j - 1].B + Temp[i - 1, j - 1].B + Temp[i - 1, j].B + Temp[i - 1, j + 1].B + Temp[i, j + 1].B) / 6;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);

                        }
                        //last row (right)
                        else if (j == Columns -1)
                        {
                            Red = (Temp[i, j].R + Temp[i-1, j].R + Temp[i-1, j - 1].R + Temp[i, j-1].R + Temp[i +1, j -1].R + Temp[i+1, j].R) / 6;
                            Green = (Temp[i, j].G + Temp[i - 1, j].G + Temp[i - 1, j - 1].G + Temp[i, j - 1].G + Temp[i + 1, j - 1].G + Temp[i + 1, j].G) / 6;
                            Blue = (Temp[i, j].B + Temp[i - 1, j].B + Temp[i - 1, j - 1].B + Temp[i, j - 1].B + Temp[i + 1, j - 1].B + Temp[i + 1, j].B) / 6;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);
                        }
                        else 

                          //// Remainder   
                        {
                            Red = (Temp[i, j].R + Temp[i - 1, j].R + Temp[i - 1, j - 1].R + Temp[i, j - 1].R + Temp[i + 1, j - 1].R + Temp[i + 1, j].R + Temp[i + 1, j + 1].R + Temp[i, j + 1].R + Temp[i - 1, j + 1].R)/9;

                            Blue = (Temp[i, j].B + Temp[i - 1, j].B + Temp[i - 1, j - 1].B + Temp[i, j - 1].B + Temp[i + 1, j - 1].B + Temp[i + 1, j].B + Temp[i + 1, j + 1].B + Temp[i, j + 1].B + Temp[i - 1, j + 1].B)/9;
                            Green = (Temp[i, j].G + Temp[i - 1, j].G + Temp[i - 1, j - 1].G + Temp[i, j - 1].G + Temp[i + 1, j - 1].G + Temp[i + 1, j].G + Temp[i + 1, j + 1].G + Temp[i, j + 1].G + Temp[i - 1, j + 1].G)/9;
                            transformedPic[i, j] = Color.FromArgb(Red, Green, Blue);

                        }

                    }
                }

                
            }

            this.Refresh();

        }


    }
}
