using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class GameTodo : Form
    {
        public int turn = -1;
        public GameTodo()
        {
            InitializeComponent();
        }

        private void GameTodo_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtgameName.Text == "" || txtMessage.Text == "")
            {
                MessageBox.Show("Not Getting Any Data!");
            }
            else
                create_NewPanel(txtgameName.Text, txtMessage.Text);
        }

        public void create_NewPanel(string name, string description)
        {
            Guna2Panel panel = new Guna2Panel();
            panel.Width = todoPanel.Width;
            panel.Height = todoPanel.Height;
            panel.FillColor = todoPanel.FillColor;
            panel.BackColor = todoPanel.BackColor;
            panel.BorderRadius = todoPanel.BorderRadius;

            Label newLabel = new Label();
            newLabel.Text = name;
            newLabel.ForeColor = txtGetgameName.ForeColor;
            newLabel.BackColor = txtGetgameName.BackColor;
            newLabel.Font = txtGetgameName.Font;
            newLabel.Location = txtGetgameName.Location; // Adjust the location as needed

            Label newDescription = new Label();
            newDescription.Text = description;
            newDescription.ForeColor = txtGetDescp.ForeColor;
            newDescription.BackColor = txtGetDescp.BackColor;
            newDescription.Font = txtGetDescp.Font;
            newDescription.Location = txtGetDescp.Location; // Adjust the location as needed

            Guna2Button discard = new Guna2Button();
            discard.Text = "Discard"; // Set the text for the discard button
            discard.Location = btnDiscard.Location; // Adjust the location as needed
            discard.Width = btnDiscard.Width;
            discard.Height = btnDiscard.Height;
            discard.Animated = true;
            discard.FillColor = btnDiscard.FillColor; // Set the fill color for the discard button
            discard.BackColor = btnDiscard.BackColor;
            discard.ForeColor = btnDiscard.ForeColor; // Set the text color for the discard button
            discard.HoverState.ForeColor = btnDiscard.HoverState.ForeColor;
            discard.HoverState.FillColor = btnDiscard.HoverState.FillColor;
            discard.BorderRadius = btnDiscard.BorderRadius; // Set the border radius for the discard button
            discard.Click += DiscardButton_Click;

            panel.Controls.Add(discard);
            panel.Controls.Add(newLabel);
            panel.Controls.Add(newDescription);
            if (turn == -1) { 
            panel.Location=todoPanel.Location;
            }else { 
            int yOffset = turn * (todoPanel.Height + 10);
            panel.Location = new Point(todoPanel.Location.X, todoPanel.Location.Y + yOffset);
            }
            todoPanel.Parent.Controls.Add(panel);
            turn++;
        }

        private void DiscardButton_Click(object? sender, EventArgs e)
        {
            turn--;
            // Get the sender as a Guna2Button
            Guna2Button clickedButton = sender as Guna2Button;

            // Check if the sender is indeed a Guna2Button
            if (clickedButton != null)
            {
                // Get the parent panel of the clicked button
                Guna2Panel parentPanel = clickedButton.Parent as Guna2Panel;

                // Check if the parent is indeed a Guna2Panel
                if (parentPanel != null)
                {
                    // Remove the parent panel from its container
                    parentPanel.Parent.Controls.Remove(parentPanel);
                }
            }
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            
           
        }
    }
}
