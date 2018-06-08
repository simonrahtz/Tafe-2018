using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Form1 : Form,IView
    {
        private Model model;
        private const int bufferDimension = 2024;
        private Bitmap buffer;
        private Label label;
        
        
        public Form1()
        {
            
            InitializeComponent();
            label = new Label();
            initGraphics();
            initialiseModel();

        }

        
        private void initGraphics()
        {

            

            buffer = new Bitmap(bufferDimension,bufferDimension);
            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;
            this.DoubleBuffered = true;

            btnDeal.Enabled = true;
            btnHit.Enabled = false;
            btnStand.Enabled = false;
                        
            label.ResetText();
            label.Font = new Font("Ariel", 15);
            this.Controls.Add(label);

            Invalidate();

            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(buffer, 0, 0);
            
        }

        public new void Handle(ViewCommand command)
        {
            command.execute(this);
        }

        private void initialiseModel()
        {
            model = new Model(this);
            model.Handle(new StartGameCommand());
            
        }

                      
        public void DrawCard(Image background, Point coord)
        {

            //resize cards
            background = new Bitmap(background, new Size(background.Width/4,background.Height/4)); 

            using (Graphics g = Graphics.FromImage(buffer))
            {
                    
                g.DrawImage(background,coord);

                if (coord.X < 400) //if location is on the right don't show card count
                {
                  SetLabel(label, model.PlayerOne.GetCardTotal.ToString(), coord.X, coord.Y-30);
                }
                

            }
            Invalidate();
        }
               

        private void btnDeal_Click(object sender, EventArgs e)
        {
            

            for (int i = 0; i < 2; i++) //deal 2 cards player and dealer
            {
                model.DealPlayer();
                model.DealDealer();
            }

            btnDeal.Enabled = false;
            btnHit.Enabled = true;
            btnStand.Enabled = true;        
            
        }

        private void ShowMessage(String text)
        {
            
               
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;

                // Displays the MessageBox.

                result = MessageBox.Show(text,"",buttons);
            

                if (result == DialogResult.OK)
                {
               
                
            }

                initGraphics();
                initialiseModel();

        }
        private void SetLabel(Label label,string text,int locationX,int locationY )
        {
            label.Location = new Point(locationX,locationY);
            label.Text = text;
        }


        private void btnHit_Click(object sender, EventArgs e)
        {
            model.DealPlayer();
            if (model.Result())
            {
                ShowMessage(model.Message);
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            btnHit.Enabled = false;
            model.StandEvent();
            ShowMessage(model.Message);
            
        }
    }
}
