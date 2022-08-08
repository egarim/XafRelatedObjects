using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System.Collections;
using System.Linq;
using System.Text;
using XafRelatedObjects.Module.BusinessObjects;

namespace XafRelatedObjects.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CustomCloneController : ViewController
    {
        SimpleAction ClonePeriod;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public CustomCloneController()
        {
            InitializeComponent();
            ClonePeriod = new SimpleAction(this, "Clone Period", "View");
            ClonePeriod.Execute += ClonePeriod_Execute;
            this.TargetObjectType = typeof(Period);

            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void ClonePeriod_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var Session = (this.ObjectSpace as XPObjectSpace).Session;
            CloneHelper cloneHelper = new CloneHelper(Session);
            Period CurrentPeriod = this.View.CurrentObject as Period;
            Period NewPeriod = this.ObjectSpace.CreateObject<Period>();
            NewPeriod.Year = CurrentPeriod.Year + 1;
            ICollection ReferenceObject = Session.CollectReferencingObjects(CurrentPeriod);
           
            foreach (BaseObject item in ReferenceObject)
            {

                if (ReferenceEquals(item, CurrentPeriod))
                    continue;

                var NewItem = cloneHelper.Clone(item);
                var PeriodMembers = NewItem.ClassInfo.PersistentProperties.Cast<XPMemberInfo>().Where(pp => pp.ReferenceType!=null && pp.ReferenceType.ClassType == typeof(Period));
                foreach (var m in PeriodMembers)
                {
                    m.SetValue(NewItem, NewPeriod);
                }
            }
            this.ObjectSpace.CommitChanges();
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
