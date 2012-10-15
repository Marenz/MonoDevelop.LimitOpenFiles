using MonoDevelop.Components.Commands;

using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;

using MonoDevelop.Core;

namespace MonoDevelop.LimitOpenFiles
{
    public enum Commands
    {
        LimitOpenfiles,
    }

    class LimitOpenFiles : CommandHandler
    {
        protected override void Run ()
        {
            base.Run();
            IdeApp.Workbench.DocumentOpened += HandleDocumentOpened;
        }
            
        protected override void Update ( CommandInfo info )
        {
        }


        protected void HandleDocumentOpened ( object sender, MonoDevelop.Ide.Gui.DocumentEventArgs e )
        { 
            if ( IdeApp.Workbench.Documents.Count > PropertyService.Get("LimitOpenFiles.limit", 5) )
            {          
                for ( int i = 0; 
                      i < IdeApp.Workbench.Documents.Count ;
                      ++i )  
                    if ( IdeApp.Workbench.Documents[i].IsDirty == false &&
                         object.ReferenceEquals(IdeApp.Workbench.ActiveDocument,IdeApp.Workbench.Documents[i]) == false && 
                         object.ReferenceEquals(e.Document,IdeApp.Workbench.Documents[i]) == false )
                    {
                        IdeApp.Workbench.Documents[i].Close();
                        break;
                    }
            }
        }
    

        public LimitOpenFiles ()
        {   
 
        }
    }
}


                   
 