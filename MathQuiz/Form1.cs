using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // Buat object dan kita beri nama 'Random'
        // untuk menghasilkan angka random
        Random randomizer = new Random();

        // 2 int berikut kita gunakan untuk
        // melakukan operasi penambahan
        int addend1;
        int addend2;

        // 2 int berikut kita gunakan untuk
        // melakukan operasi pengurangan 
        int minuend;
        int subtrahend;

        // 2 int berikut kita gunakan untuk
        // melakukan operasi perkalian
        int multiplicand;
        int multiplier;

        // 2 int berikut kita gunakan untuk
        // melakukan operasi pembagian
        int dividend;
        int divisor;

        // integer berikut digunakan untuk
        // menyimpan waktu yang tersisa
        int timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fungsi berikut digunakan untuk
        /// memulai quiz dengan mengisi semua soal perhitungan
        /// dan memulai timer
        /// </summary>
        public void StartTheQuiz()
        {
            // Mengisi soal penambahan
            // Menghasilkan 2 angka random untuk ditambahkan
            // range angka random dimulai dari 0 sampai dengan 50
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Mengonversi 2 angka random yang sudah dihasilkan
            // menjadi tipe data string jadi angka-angka tersebut
            // bisa ditampilkan ke dalam label
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' adalah nama dari NumericUpDown 
            // untuk soal penambahan
            // langkah berikut memastikan nilai 'sum' adalah 0
            // sebelum kita memasukan value ke dalamnya
            sum.Value = 0;

            // Mengisi soal pengurangan
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Mengisi soal perkalian
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Mengisi soal pembagian
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Mulai timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
            timeLabel.BackColor = Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // Jika fungsi CheckTheAnswer() mengembalikan nilai true
                // artinya jawaban user sudah benar, dan hentikan timer
                // dan tampilkan MessageBox
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Jika fungsi CheckTheAnswer() mengembalikan nilai false
                // lanjutkan menghitung mundur timer.
                // kurangi waktunya 1 persatu dan
                // tampilkan waktu tersisa dengan cara memperbarui label Time Left
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
                if (timeLeft <= 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }
            else
            {
                // Jika user kehabisan waktu, hentikan timer dan
                // tampilkan MessageBox dan isi jawaban yang benar
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        /// <summary>
        /// Cek jawaban jika user sudah mengisi kolom jawaban
        /// </summary>
        /// <returns>True jika jawabannya benar, false jika sebaliknya.</returns>
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

        private void answer_enter(object sender, EventArgs e)
        {
            // Select semua jawaban di dalam NumericUpDown
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
