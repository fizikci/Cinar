using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace Cinar.UICommands
{
    public class Command
    {
        public Command()
        { 
        }
        public Command(Action<string> execute)
            : this()
        {
            this.execute = execute;
        }
        public Command(Action<string> execute, Func<bool> isEnabled)
            : this(execute)
        {
            this.isEnabled = isEnabled;
        }
        public Command(Action<string> execute, Func<bool> isEnabled, Func<bool> isVisible)
            : this(execute, isEnabled)
        {
            this.isVisible = isVisible;
        }

        private string name;
        public string Name {
            get
            {
                if (this.name == null)
                    this.name = this.Execute.Method.Name;
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        private string displayName;
        public string DisplayName {
            get { if (displayName == null) displayName = this.Name; return displayName; }
            set { displayName = value; } 
        }

        private bool visibleOnToolbar = true;
        public bool VisibleOnToolbar { 
            get { return visibleOnToolbar; } 
            set { visibleOnToolbar = value; } 
        }

        Action<string> execute;
        public Action<string> Execute
        {
            get {
                if (execute == null)
                    execute = (string arg) => { throw new NotImplementedException(); };
                return execute; 
            }
            set { execute = value; }
        }

        Func<bool> isEnabled;
        public Func<bool> IsEnabled
        {
            get
            {
                if (isEnabled == null)
                    isEnabled = () => { return true; };
                return isEnabled;
            }
            set { isEnabled = value; }
        }

        Func<bool> isVisible;
        public Func<bool> IsVisible
        {
            get
            {
                if (isVisible == null)
                    isVisible = () => { return true; };
                return isVisible;
            }
            set { isVisible = value; }
        }

        private CommandTrigger trigger;
        public CommandTrigger Trigger 
        { 
            get 
            {
                if (trigger == null)
                {
                    trigger = new CommandTrigger();
                    if (triggers == null)
                        triggers = new List<CommandTrigger>();
                    if (triggers.Count == 0)
                        triggers.Add(trigger);
                }
                return trigger;
            } 
            set 
            {
                trigger = value;
                if (triggers == null)
                    triggers = new List<CommandTrigger>();
                if (triggers.Count == 0)
                    triggers.Add(trigger);
            } 
        }

        private List<CommandTrigger> triggers;
        public List<CommandTrigger> Triggers 
        { 
            get 
            {
                if (triggers == null)
                {
                    triggers = new List<CommandTrigger>();
                    if (trigger != null)
                        triggers.Add(trigger);
                }
                return triggers;
            } 
            set 
            {
                triggers = value;
            } 
        }
    }

    public class CommandTrigger
    {
        public Component Control { get; set; }
        public string Event { get; set; }
        public string Argument { get; set; }
    }

    public class CommandCollection : List<Command>
    {
        public CommandCollection() { }
        public CommandCollection(params Command[] commands)
        {
            if(commands!=null)
                foreach(Command cmd in commands)
                    this.Add(cmd);
        }
        public Command this[string cmdName]
        {
            get {
                foreach (Command c in this)
                    if (c.Name == cmdName)
                        return c;
                return null;
            }
        }
    }

    public class CommandManager
    {
        private CommandCollection commands = new CommandCollection();
        public CommandCollection Commands {
            get { return commands; }
            set { commands = value; } 
        }

        public EventArgs LastEventArgs;
        public object LastSender;

        public Action BeforeCommandExecute;
        public Action AfterCommandExecute;

        public void SetCommandTriggers()
        {
            foreach (Command command in commands)
                foreach (CommandTrigger trigger in command.Triggers)
                {
                    if (string.IsNullOrEmpty(trigger.Event))
                    {
                        object[] attribs = trigger.Control.GetType().GetCustomAttributes(typeof(DefaultEventAttribute), true);
                        if (attribs == null || attribs.Length == 0)
                            continue;
                        DefaultEventAttribute attrib = (DefaultEventAttribute)attribs[0];
                        trigger.Event = attrib.Name;
                    }

                    Tracer tracer = new Tracer(trigger.Control, new Tracer.OnEventHandler(onEventTriggered));

                    tracer.HookEvent(trigger.Event);
                }
        }
        private void onEventTriggered(object sender, object target, string eventName, EventArgs e)
        {
            foreach (Command command in commands)
                foreach (CommandTrigger trigger in command.Triggers)
                {
                    if (trigger.Event == eventName && trigger.Control == target)
                    {
                        this.LastEventArgs = e;
                        this.LastSender = target;
                        if (this.BeforeCommandExecute != null)
                            this.BeforeCommandExecute();
                        command.Execute(trigger.Argument);
                        if (this.AfterCommandExecute != null)
                            this.AfterCommandExecute();
                        this.SetCommandControlsEnable();
                    }
                }
        }

        public Action<Component, bool> ControlHasNoVisibleProperty;

        public void SetCommandControlsVisibility(Type controlType)
        {
            foreach (Command command in commands)
                foreach (CommandTrigger trigger in command.Triggers)
                {
                    Type cmdControlType = trigger.Control.GetType();
                    if (controlType != null && controlType != cmdControlType)
                        continue;

                    PropertyInfo pi = cmdControlType.GetProperty("Visible");
                    if (pi == null)
                    {
                        if (this.ControlHasNoVisibleProperty != null)
                            this.ControlHasNoVisibleProperty(trigger.Control, command.IsVisible());
                    }
                    else
                    {
                        pi.SetValue(trigger.Control, command.IsVisible(), null);
                    }
                }
        }
        public void SetCommandControlsVisibility()
        {
            this.SetCommandControlsVisibility(null);
        }

        public Action<Component, bool> ControlHasNoEnabledProperty;

        public void SetCommandControlsEnable(Type controlType)
        {
            foreach (Command command in commands)
                foreach (CommandTrigger trigger in command.Triggers)
                {
                    Type cmdControlType = trigger.Control.GetType();
                    if (controlType != null && controlType != cmdControlType)
                        continue;

                    PropertyInfo pi = cmdControlType.GetProperty("Enabled");
                    if (pi == null)
                    {
                        if (this.ControlHasNoEnabledProperty != null)
                            this.ControlHasNoEnabledProperty(trigger.Control, command.IsEnabled());
                    }
                    else
                    {
                        pi.SetValue(trigger.Control, command.IsEnabled(), null);
                    }
                }
        }
        public void SetCommandControlsEnable() {
            this.SetCommandControlsEnable(null);
        }
    }
}