using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using GridCommon;
using Microsoft.Win32;

namespace GridServer
{
    class MainWindowModel : INotifyPropertyChanged
    {
        private Visibility loadingIndicatorVisibility = Visibility.Visible;
        private Visibility openFileVisibility = Visibility.Visible;
        private string selectedFileText;
        private string selctedFilePath;
        private bool fileSelected;
        private Visibility resultVisibility;
        private string resultText;
        private int pathLength;
        public Visibility LoadingIndicatorVisibility
        {
            get => loadingIndicatorVisibility;
            set
            {
                loadingIndicatorVisibility = value;
                OnPorpertyChanged("LoadingIndicatorVisibility");
            }
        }
        public Visibility OpenFileVisibility
        {
            get => openFileVisibility;
            set
            {
                openFileVisibility = value;
                OnPorpertyChanged("OpenFileVisibility");
            }
        }

        public Visibility ResultVisibility
        {
            get => resultVisibility;
            set
            {
                resultVisibility = value;
                OnPorpertyChanged("ResultVisibility");
            }
        }

        public string ResultText
        {
            get => resultText;
            set
            {
                resultText = value.ToString();
                OnPorpertyChanged("ResultText");
            }
        }

        public string SelectedFileText
        {
            get => selectedFileText;
            set
            {
                selectedFileText = value;
                OnPorpertyChanged("SelectedFileText");
            }
        }

        public bool IsFileSelected
        {
            get => fileSelected;
            set
            {
                fileSelected = value;
                OnPorpertyChanged("IsFileSelected");
            }
        }

        public ICommand OpenFileBtnCommand { get; set; }
        public ICommand StartCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPorpertyChanged(string prop)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public int PathLength
        {
            get => pathLength;
            set
            {
                pathLength = value;
                OnPorpertyChanged("PathLength");
            }
        }
        public MainWindowModel()
        {
            openFileVisibility = Visibility.Visible;
            loadingIndicatorVisibility = Visibility.Collapsed;
            resultVisibility = Visibility.Collapsed;
            selectedFileText = "не выбрано";
            OpenFileBtnCommand = new MainCommand(OpenFileBtn);
            StartCommand = new MainCommand(Start);
        }

        
        public void OpenFileBtn()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFileText = System.IO.Path.GetFileName(openFileDialog.FileName);
                selctedFilePath = openFileDialog.FileName;
                IsFileSelected = true;
            }
        }

        public void Start()
        {
            try
            {
                var parser = new SquareMatrixParser();
                var matrix = parser.ParseStream(System.IO.File.OpenRead(selctedFilePath));
                if (PathLength < 3)
                {

                    MessageBox.Show("Длина пути некорректна", "Предупреждение");
                    return;
                }
                else
                {
                    if (PathLength > matrix.GetLength(0))
                    {

                        MessageBox.Show("Длина пути некорректна", "Предупреждение");
                        return;
                    }
                    else
                    {
                        LoadingIndicatorVisibility = Visibility.Visible;
                        OpenFileVisibility = Visibility.Collapsed;
                        var server = new Server();
                        server.Start(matrix, PathLength, OnResult);
                    }
                }
            }
            catch (Exception e)
            {
                LoadingIndicatorVisibility = Visibility.Collapsed;
                OpenFileVisibility = Visibility.Visible;
                MessageBox.Show(e.Message, "Ошибка!");
                Console.WriteLine(e);
            }
        }

        void OnResult(string result)
        {
            LoadingIndicatorVisibility = Visibility.Collapsed;
            OpenFileVisibility = Visibility.Collapsed;
            ResultVisibility = Visibility.Visible;
            ResultText = result;
        }

    }
}
