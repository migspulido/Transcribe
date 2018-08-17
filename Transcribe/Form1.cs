///Miguel Pulido - Systems Architect

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.IO.Ports;


namespace Transcribe
{
    public partial class Transcribe : Form
    {

        //use StreamWriter class
        //The line directly below works, second param "true" appends

        StreamWriter w = new StreamWriter(@"C:\\TestFolder\Transcribe.txt",true);
        
               

        public Transcribe()
        {
            InitializeComponent();

        }
        //Setup our variables for use in our textbox
        SpeechSynthesizer sSynth = new SpeechSynthesizer();
        PromptBuilder pBuilder = new PromptBuilder();
        SpeechRecognitionEngine sRecognize = new SpeechRecognitionEngine();


        private void button1_Click(object sender, EventArgs e)
        {
            pBuilder.ClearContent();
            pBuilder.AppendText(TextBox1.Text);
            sSynth.Speak(pBuilder);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //timer1.Start();

            //setup button toggle for enabling/disabling listening
            button2.Enabled = false;
            button3.Enabled = true;
            //Set Grammar to recognize words you identify below
            Choices sList = new Choices();
            
            
            
            
            sList.Add(new string[] {"open", "close", "help", "fire", "earthquake", "call 911"});
            
            //Create Grammar based on our sList definition above
            Grammar gr = new Grammar(new GrammarBuilder(sList));
            
            // Setup exception handling
            try
            {
                sRecognize.RequestRecognizerUpdate();
                sRecognize.LoadGrammar(gr);
                sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;
                //sets default microphone
                sRecognize.SetInputToDefaultAudioDevice();
                //sets multi-word recognition 
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {

                return;
            }
        }

        //sets up even handler - when exit is heard, exit application 
        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "exit")
            {
                Application.Exit(); // listens for exit then closes application

            }
            else
            {
                TextBox1.Text = TextBox1.Text + " " + e.Result.Text.ToString();
            }
            
            // MessageBox.Show("speech recognized:" + e.Result.Text.ToString()); // opens message box
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sRecognize.RecognizeAsyncStop();
            button2.Enabled = true;
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
        //writer.WriteLine();
            w.Close();

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //Use writeline method to write the text and 
            //in para... put your text, i have used textBox1's text 

            w.WriteLine(TextBox1.Text);

            //always close your stream

       //     w.Close();
        }




        }
    }

