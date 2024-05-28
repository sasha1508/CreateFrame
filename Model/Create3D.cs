using CoreAddIn;
using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CreateFrame.Model
{
    public class Create3D
    {
        private static Inventor.Application? ThisApplication { get; set; } = Globals.InvApp;

        /// <summary>
        /// Создаём пространственную раму
        /// </summary>
        /// <param name="length"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void CreateFrame(double length, double width, double height)
        {
            if (ThisApplication != null)
            {
                //Создание документа детали:
                string TemplatePath = "C:\\ProgramData\\Autodesk\\ApplicationPlugins\\CreateFrame\\x64\\Debug\\net8.0-windows7.0\\Угол 8-100 ГОСТ 8509-93.ipt";
                Documents docs = ThisApplication.Documents;
                Document doc = docs.Add(DocumentTypeEnum.kPartDocumentObject, TemplatePath, false);
                doc.Save();

                Matrix oPositionMatrix = ThisApplication.TransientGeometry.CreateMatrix();

                string sFileName = doc.FullFileName;

                ComponentOccurrence? oPart1_1 = ((ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix));
                ComponentOccurrence? oPart1_2 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart1_3 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart1_4 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);



                AssemblyComponentDefinition? oFaceDef = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition;


                //Накладываем зависимости:

                var oConstr1 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart1_2?.SurfaceBodies[1].Faces[3], -height / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                var oConstr2 = oFaceDef?.Constraints.AddMateConstraint(oPart1_3?.SurfaceBodies[1].Faces[3], oPart1_4?.SurfaceBodies[1].Faces[3], -height / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                var oConstr3 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart1_3?.SurfaceBodies[1].Faces[4], -length / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                var oConstr4 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[4], oPart1_4?.SurfaceBodies[1].Faces[4], -length / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                var ofConstr1 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart1_2?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr1?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart1_2?.SurfaceBodies[1].Faces[4], 0);
                var ofConstr2 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart1_4?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr2?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart1_4?.SurfaceBodies[1].Faces[3], 0);

            }
        }
    }
}
