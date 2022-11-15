using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using System.Xml.Linq;
//Task 2
namespace Lab1PlaceGroup
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get application and document objects
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            //Define a reference Object to accept the pick result
            Reference myRef = null;

            Selection sel = uiapp.ActiveUIDocument.Selection;

            myRef = sel.PickObject(ObjectType.Face, "Select a face");
            Element e = doc.GetElement(myRef);
            //Getting the face
            Face face = e.GetGeometryObjectFromReference(myRef) as Face;
            
            
            
            string r="",g=" ",b=" ",n=" ";

             List<Element> materials = new FilteredElementCollector(doc).OfClass(typeof(Material)).ToList();

            //searching for a match of face material id in all the material ids. 
            try
            {
                foreach (Material material in materials)
                {

                    if (face.MaterialElementId == material.Id)
                    {
                        r = material.Color.Red.ToString();
                        g = material.Color.Green.ToString();
                        b = material.Color.Blue.ToString();
                        n = material.Name;

                    }



                }
                TaskDialog.Show("Material information", "Material Name: " + n + ", RGB" + r + "," + g + "," + b);
            }
            catch(Exception ex)
            {
                TaskDialog.Show("Exception Caught", "Exception Caught!: " + ex.Message);
            }
            
            /*

            //Define a reference Object to accept the pick result
            Reference pickedref = null;
            //Pick a group
            Selection sel = uiapp.ActiveUIDocument.Selection;
            pickedref = sel.PickObject(ObjectType.Element, "Please select a group");
            Element elem = doc.GetElement(pickedref);
            Group group = elem as Group;

            //Pick point
            XYZ point = sel.PickPoint("Please pick a point to place group");

            //Place the group
            Transaction trans = new Transaction(doc);
            trans.Start("Lab");
            doc.Create.PlaceGroup(point, group.GroupType);
            trans.Commit();
            */
            return Result.Succeeded;
        }
    }
}