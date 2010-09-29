
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.VersionControl.Views
{
	public partial class LogWidget
	{
		private global::Gtk.UIManager UIManager;
		
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Toolbar commandBar;
		
		private global::Gtk.VPaned vpaned1;
		
		private global::Gtk.HPaned hpaned1;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.TreeView treeviewLog;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.ScrolledWindow scrolledwindow1;
		
		private global::Gtk.TextView textviewDetails;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow1;
		
		private global::Gtk.TreeView treeviewFiles;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.VersionControl.Views.LogWidget
			Stetic.BinContainer w1 = global::Stetic.BinContainer.Attach (this);
			this.UIManager = new global::Gtk.UIManager ();
			global::Gtk.ActionGroup w2 = new global::Gtk.ActionGroup ("Default");
			this.UIManager.InsertActionGroup (w2,0);
			this.Name = "MonoDevelop.VersionControl.Views.LogWidget";
			// Container child MonoDevelop.VersionControl.Views.LogWidget.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><toolbar name='commandBar'/></ui>");
			this.commandBar = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/commandBar")));
			this.commandBar.Name = "commandBar";
			this.commandBar.ShowArrow = false;
			this.commandBar.ToolbarStyle = ((global::Gtk.ToolbarStyle)(3));
			this.commandBar.IconSize = ((global::Gtk.IconSize)(1));
			this.vbox1.Add (this.commandBar);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.commandBar]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.vpaned1 = new global::Gtk.VPaned ();
			this.vpaned1.CanFocus = true;
			this.vpaned1.Position = 204;
			// Container child vpaned1.Gtk.Paned+PanedChild
			this.hpaned1 = new global::Gtk.HPaned ();
			this.hpaned1.CanFocus = true;
			this.hpaned1.Name = "hpaned1";
			this.hpaned1.Position = 236;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewLog = new global::Gtk.TreeView ();
			this.treeviewLog.CanFocus = true;
			this.treeviewLog.Name = "treeviewLog";
			this.GtkScrolledWindow.Add (this.treeviewLog);
			this.hpaned1.Add (this.GtkScrolledWindow);
			global::Gtk.Paned.PanedChild w5 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.GtkScrolledWindow]));
			w5.Resize = false;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Details");
			this.vbox2.Add (this.label1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.label1]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.scrolledwindow1 = new global::Gtk.ScrolledWindow ();
			this.scrolledwindow1.CanFocus = true;
			this.scrolledwindow1.Name = "scrolledwindow1";
			this.scrolledwindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledwindow1.Gtk.Container+ContainerChild
			this.textviewDetails = new global::Gtk.TextView ();
			this.textviewDetails.CanFocus = true;
			this.textviewDetails.Name = "textviewDetails";
			this.scrolledwindow1.Add (this.textviewDetails);
			this.vbox2.Add (this.scrolledwindow1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.scrolledwindow1]));
			w8.Position = 1;
			this.hpaned1.Add (this.vbox2);
			this.vpaned1.Add (this.hpaned1);
			global::Gtk.Paned.PanedChild w10 = ((global::Gtk.Paned.PanedChild)(this.vpaned1 [this.hpaned1]));
			w10.Resize = false;
			// Container child vpaned1.Gtk.Paned+PanedChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeviewFiles = new global::Gtk.TreeView ();
			this.treeviewFiles.CanFocus = true;
			this.treeviewFiles.Name = "treeviewFiles";
			this.GtkScrolledWindow1.Add (this.treeviewFiles);
			this.vpaned1.Add (this.GtkScrolledWindow1);
			this.vbox1.Add (this.vpaned1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.vpaned1]));
			w13.Position = 1;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			w1.SetUiManager (UIManager);
			this.Show ();
		}
	}
}
