using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcCommands
{


    public partial class testForm : Form
    {
        IReciever calculator = null;
        ACommand command = null;
        AddCommand addCmd = null;
        SubtractCommand subCmd = null;
        MultiplyCommand mulCmd = null;
        public testForm()
        {
            InitializeComponent();
        }

        private void testForm_Load(object sender, EventArgs e)
        {
            calculator = new Calculator(20, 10);

            
        }

        private void calculate_Click_1(object sender, EventArgs e)
        {
            label4.Text = "Result: " + command.Execute().ToString();

        }

        private void radioAdd_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioAdd.Checked == true)
            {
                command = new AddCommand(calculator);
               
            }
            else if (radioSub.Checked == true)
            {
                command =  new SubtractCommand(calculator);
            }
            else if (radioMultiply.Checked == true)
            {
                command = mulCmd = new MultiplyCommand(calculator);
                
            }
        }
    }

    //This is a helper type created to decide inside reciever
    enum ActionList
    {
        ADD,
        SUBTRACT,
        MULTIPLY
    }

    interface IReciever
    {
        void SetAction(ActionList action);
        int GetResult();
    }
    abstract class ACommand
    {
        protected IReciever reciever_ = null;

        public ACommand(IReciever reciever)
        {
            reciever_ = reciever;
        }

        public abstract int Execute();
    }

    class AddCommand : ACommand
    {
        public AddCommand(IReciever reciever)
            : base(reciever)
        {

        }
        public override int Execute()
        {
            try
            {
                reciever_.SetAction(ActionList.ADD);
                return reciever_.GetResult();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }

    class SubtractCommand : ACommand
    {
        public SubtractCommand(IReciever reciever)
            : base(reciever)
        {

        }
        public override int Execute()
        {
            reciever_.SetAction(ActionList.SUBTRACT);
            return reciever_.GetResult();
        }
    }

    class MultiplyCommand : ACommand
    {
        public MultiplyCommand(IReciever reciever)
            : base(reciever)
        {

        }
        public override int Execute()
        {
            reciever_.SetAction(ActionList.MULTIPLY);
            return reciever_.GetResult();
        }
    }

    class Calculator : IReciever
    {
        int x_;
        int y_;

        ActionList currentAction;

        public Calculator(int x, int y)
        {
            x_ = x;
            y_ = y;
        }

        #region IReciever Members

        public void SetAction(ActionList action)
        {
            currentAction = action;
        }

        public int GetResult()
        {
            int result;
            if (currentAction == ActionList.ADD)
            {
                result = x_ + y_;

            }
            else if (currentAction == ActionList.MULTIPLY)
            {
                result = x_ * y_;
            }
            else
            {
                result = x_ - y_;
            }
            return result;
        }

        #endregion
    }
}
