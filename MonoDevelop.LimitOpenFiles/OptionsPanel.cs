using MonoDevelop.Components.Commands;

using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide.Gui.Dialogs;
using MonoDevelop.Core;

namespace MonoDevelop.LimitOpenFiles
{
    class OptionsPanel : Gtk.Bin, IOptionsPanel
    {
        private global::Gtk.Label label;
        private global::Gtk.Entry textfield;
        private global::Gtk.HBox hbox;
 
        public virtual Gtk.Widget CreatePanelWidget()
        {
            return hbox;
        }

        public virtual void ApplyChanges ( )
        {
            int num = PropertyService.Get("LimitOpenFiles.limit", 5);
            int.TryParse(this.textfield.Text, out num);

            PropertyService.Set("LimitOpenFiles.limit", num);
        }

        public void Initialize ( OptionsDialog dialog, object dataObject )
        {
        }

        public bool IsVisible ()
        {
            return true;
        }

        public bool ValidateChanges ( )
        {
            int num = -1;
            int.TryParse(this.textfield.Text, out num);

            if ( num < 2 ) return false;

            return true;
        }

        public OptionsPanel ()
        {
            this.Build();
        }

        void Build()
        {
            this.Name = "MonoDevelop.SourceEditor.OptionPanels.LimitOpenFiles";

            this.hbox = new global::Gtk.HBox();
            this.hbox.Name = "HBox";

            this.label = new global::Gtk.Label();
            this.label.Name = "Description";
            this.label.LabelProp = "Max. amount of concurrent open files:";
            this.textfield = new global::Gtk.Entry();
            this.textfield.Text =  PropertyService.Get("LimitOpenFiles.limit", 5).ToString();
            this.hbox.Add(this.label);
            this.hbox.Add(this.textfield);

            this.hbox.ShowAll();
        }
    }
}

