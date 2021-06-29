using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace TimedMathQuiz
{
    public partial class Form1 : Form
    {
        // Create a randpom object called randomizer to generate random number.
        Random randomizer = new Random();

        // store numbers to use in the addition.
        int addend1;
        int addend2;

        // store numbers to use in the subtraction
        int minuend;
        int subtrahend;

        //store numers to use in the multiplication
        int multiplicand;
        int multiplier;

        //store numbers to use in the division
        int dividend;
        int divisor;

        // this keeps track of the remaining time
        int timeLeft;

        /*
         Start the quiz by filling in all of the problems 
        and starting the timer
         */
        public void StartTheQuiz()
        {
            // fill in the addition problem
            // generate two random numbers to add.
            // store those values in variables addend1 and addend 2
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // convert the two randomly generated numbers
            // into strings so that they can be displayed
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // this ensures the sum's value is zero before adding more values to it
            sum.Value = 0;

            //Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // fill in the multiplication problem
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // fill in the division problem
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            divideLeftLabel.Text = dividend.ToString();
            divideRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /*
         * Check the answer to see if user got it right
         * true if the answer's correct, false otherwise
         */
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) 
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void starButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                //if return is true, the user got the answer right .
                //stop the timer and show a messagebox.
                timer1.Stop();
                playWinSound();
                MessageBox.Show("You got all the anwers right!", "Nerd!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // display time left by updating the timeleft label
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";

                if (timeLeft < 6)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                //if the user ran out of time, stop the timer
                // and show a MessageBox, and fill in the asnwer.
                timer1.Stop();
                playFailSound();
                timeLabel.BackColor = DefaultBackColor;
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You ran out of time, Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void playWinSound()
        {
            SoundPlayer winSound = new SoundPlayer(@"c:\Windows\Media\tada.wav");
            winSound.Play();
        }
        private void playFailSound()
        {
            SoundPlayer playFailSound = new SoundPlayer(@"c:\Windows\Media\Windows Critical Stop.wav");
            playFailSound.Play();
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        // applies green background color for each entry  that is answered correctly
        private void sumCorrect(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (addend1 + addend2 == sum.Value)
            {
                answerBox.BackColor = Color.GreenYellow;
            }
            else
            {
                answerBox.BackColor = DefaultBackColor;
            }
        }

        private void subtractionCorrect(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (minuend - subtrahend == difference.Value)
            {
                answerBox.BackColor = Color.GreenYellow;
            }
            else
            {
                answerBox.BackColor = DefaultBackColor;
            }
        }
        private void productCorrect(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (multiplicand * multiplier == product.Value)
            {
                answerBox.BackColor = Color.GreenYellow;
            }
            else
            {
                answerBox.BackColor = DefaultBackColor;
            }
        }

        private void divisionCorrect(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (dividend / divisor == quotient.Value)
            {
                answerBox.BackColor = Color.GreenYellow;
            }
            else
            {
                answerBox.BackColor = DefaultBackColor;
            }
        }
    }
}
