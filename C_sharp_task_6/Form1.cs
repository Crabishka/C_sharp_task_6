using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TaskLibrary;


namespace C_sharp_task_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Выберите .dll файл";
                    dialog.Filter = "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*";

                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                    {
                        string selectedFilePath = dialog.FileName;
                        label2.Text = selectedFilePath;
                        Assembly assembly = Assembly.LoadFrom(selectedFilePath);
                        Type interfaceType = assembly.GetType("ClassLibrary.Paper");

                        Type[] types = assembly.GetTypes();


                        foreach (Type type in types)
                        {
                            if (interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                            {
                                comboBox2.Items.Add(type.FullName);
                                richTextBox1.Text += type.ToString() + "\n";
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                Console.WriteLine("Ошибка загрузки типов:");
                foreach (Exception loaderException in ex.LoaderExceptions)
                {
                    richTextBox1.Text += loaderException.Message;
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string className = comboBox.SelectedItem.ToString();
            Assembly assembly = Assembly.LoadFile(label2.Text);
            Type classType = assembly.GetType(className);
            MethodInfo[] methods = classType.GetMethods();

            comboBox1.Items.Clear();
            foreach (MethodInfo method in methods)
            {
                comboBox1.Items.Add(method.Name);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string var1 = textBox1.Text;
            string var2 = textBox2.Text;
            string var3 = textBox3.Text;
            string var4 = textBox4.Text;

            string className = comboBox2.SelectedItem.ToString();
            Assembly assembly = Assembly.LoadFile(label2.Text);
            Type classType = assembly.GetType(className);
            ConstructorInfo constructor = classType.GetConstructors()[0];
            object[] arguments = { "Johny", "Johny", "Johny", null, null };
            object instance = constructor.Invoke(arguments);
            string methodName = comboBox1.SelectedItem.ToString();
            MethodInfo method = classType.GetMethod(methodName);

            if (var1 != "" && var2 != "" && var3 != "" && var4 != "")
            {
                richTextBox1.Text = method.Invoke(instance, new[] { var1, var2, var3, var4 }).ToString();
            }
            else if (var1 != "" && var2 != "" && var3 != "")
            {
                richTextBox1.Text = method.Invoke(instance, new[] { var1, var2, var3 }).ToString();
            }
            else if (var1 != "" && var2 != "")
            {
                richTextBox1.Text = method.Invoke(instance, new[] { var1, var2 }).ToString();
            }
            else if (var1 != "")
            {
                richTextBox1.Text = method.Invoke(instance, new[] { var1 }).ToString();
            }
            else
            {
                richTextBox1.Text = method.Invoke(instance, null).ToString();
            }
        }
    }
}