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
                AssemblyComponentDefinition? oFaceDef = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition;

                //Создание поперечного уголка:
                string TemplatePath = "C:\\ProgramData\\Autodesk\\ApplicationPlugins\\CreateFrame\\x64\\Debug\\net8.0-windows7.0\\Угол 8-100 ГОСТ 8509-93.ipt";
                Documents docs = ThisApplication.Documents;

                Document doc1 = docs.Add(DocumentTypeEnum.kPartDocumentObject, TemplatePath, false);

                (doc1 as PartDocument).ComponentDefinition.Parameters["B_L"].Value = width / 10;

                doc1.Save();

                Matrix oPositionMatrix = ThisApplication.TransientGeometry.CreateMatrix();

                string sFileName = doc1.FullFileName;

                ComponentOccurrence? oPart1_1 = ((ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix));
                ComponentOccurrence? oPart1_2 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart1_3 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart1_4 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);


                //Создание продольного уголка:
                Document doc2 = docs.Add(DocumentTypeEnum.kPartDocumentObject, TemplatePath, false);
                (doc2 as PartDocument).ComponentDefinition.Parameters["B_L"].Value = length / 10;
                doc2.Save();

                sFileName = doc2.FullFileName;

                ComponentOccurrence? oPart2_1 = ((ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix));
                ComponentOccurrence? oPart2_2 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart2_3 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart2_4 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);


                //Создание вертикального уголка:
                Document doc3 = docs.Add(DocumentTypeEnum.kPartDocumentObject, TemplatePath, false);
                (doc3 as PartDocument).ComponentDefinition.Parameters["B_L"].Value = height / 10;
                doc3.Save();

                sFileName = doc3.FullFileName;

                ComponentOccurrence? oPart3_1 = ((ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix));
                ComponentOccurrence? oPart3_2 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart3_3 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);
                ComponentOccurrence? oPart3_4 = (ThisApplication.ActiveDocument as AssemblyDocument)?.ComponentDefinition.Occurrences.Add(sFileName, oPositionMatrix);


                //Накладываем зависимости:
                 //Между продольными уголками:
                var oConstr1 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart1_2?.SurfaceBodies[1].Faces[3], -height / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                var oConstr2 = oFaceDef?.Constraints.AddMateConstraint(oPart1_3?.SurfaceBodies[1].Faces[3], oPart1_4?.SurfaceBodies[1].Faces[3], -height / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                var oConstr3 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart1_3?.SurfaceBodies[1].Faces[4], -length / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                var oConstr4 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[4], oPart1_4?.SurfaceBodies[1].Faces[4], -length / 10, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                
                var ofConstr1 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart1_2?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr1?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart1_2?.SurfaceBodies[1].Faces[4], 0);
                var ofConstr2 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart1_4?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr2?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart1_4?.SurfaceBodies[1].Faces[3], 0);
                var ofConstr3 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart1_3?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr3?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart1_3?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr4 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart1_2?.SurfaceBodies[1].Faces[11], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr4?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart1_2?.SurfaceBodies[1].Faces[11], 0);
                var ofConstr5= oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart1_4?.SurfaceBodies[1].Faces[11], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr5?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart1_4?.SurfaceBodies[1].Faces[11], 0);


                //Между продольными и поперечными уголками:
                var ofConstr6 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart2_1?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr6?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart2_1?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr7 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[11], oPart2_1?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr7?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[11], oPart2_1?.SurfaceBodies[1].Faces[4], 0);
                var ofConstr8 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart2_1?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr8?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart2_1?.SurfaceBodies[1].Faces[3], 0);
                
                var ofConstr9 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[4], oPart2_2?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr9?.ConvertToFlushConstraint(oPart1_2?.SurfaceBodies[1].Faces[4], oPart2_2?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr10 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[10], oPart2_2?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr10?.ConvertToFlushConstraint(oPart1_2?.SurfaceBodies[1].Faces[10], oPart2_2?.SurfaceBodies[1].Faces[3], 0);
                var ofConstr11 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[3], oPart2_2?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr11?.ConvertToFlushConstraint(oPart1_2?.SurfaceBodies[1].Faces[3], oPart2_2?.SurfaceBodies[1].Faces[4], 0);

                var ofConstr12 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart2_3?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr12?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart2_3?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr13 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart2_3?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr13?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart2_3?.SurfaceBodies[1].Faces[3], 0);
                var ofConstr14 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart2_3?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr14?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart2_3?.SurfaceBodies[1].Faces[4], 0);

                var ofConstr15 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[4], oPart2_4?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr15?.ConvertToFlushConstraint(oPart1_2?.SurfaceBodies[1].Faces[4], oPart2_4?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr16 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[11], oPart2_4?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr16?.ConvertToFlushConstraint(oPart1_2?.SurfaceBodies[1].Faces[11], oPart2_4?.SurfaceBodies[1].Faces[4], 0);
                var ofConstr17 = oFaceDef?.Constraints.AddMateConstraint(oPart1_2?.SurfaceBodies[1].Faces[3], oPart2_4?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr17?.ConvertToFlushConstraint(oPart1_2?.SurfaceBodies[1].Faces[3], oPart2_4?.SurfaceBodies[1].Faces[3], 0);


                //Между продольными и вертикальными уголками:
                var ofConstr18 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_1?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr18?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_1?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr19 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart3_1?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr19?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart3_1?.SurfaceBodies[1].Faces[4], 0);
                var ofConstr20 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[11], oPart3_1?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr20?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[11], oPart3_1?.SurfaceBodies[1].Faces[3], 0);

                var ofConstr21 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_2?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr21?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_2?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr22 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart3_2?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr22?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[4], oPart3_2?.SurfaceBodies[1].Faces[3], 0);
                var ofConstr23 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart3_2?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr23?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[10], oPart3_2?.SurfaceBodies[1].Faces[4], 0);

                var ofConstr24 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_3?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr24?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_3?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr25 = oFaceDef?.Constraints.AddMateConstraint(oPart1_4?.SurfaceBodies[1].Faces[4], oPart3_3?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr25?.ConvertToFlushConstraint(oPart1_4?.SurfaceBodies[1].Faces[4], oPart3_3?.SurfaceBodies[1].Faces[4], 0);
                var ofConstr26 = oFaceDef?.Constraints.AddMateConstraint(oPart1_4?.SurfaceBodies[1].Faces[11], oPart3_3?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr26?.ConvertToFlushConstraint(oPart1_4?.SurfaceBodies[1].Faces[11], oPart3_3?.SurfaceBodies[1].Faces[3], 0);

                var ofConstr27 = oFaceDef?.Constraints.AddMateConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_4?.SurfaceBodies[1].Faces[10], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr27?.ConvertToFlushConstraint(oPart1_1?.SurfaceBodies[1].Faces[3], oPart3_4?.SurfaceBodies[1].Faces[10], 0);
                var ofConstr28 = oFaceDef?.Constraints.AddMateConstraint(oPart1_4?.SurfaceBodies[1].Faces[4], oPart3_4?.SurfaceBodies[1].Faces[3], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr28?.ConvertToFlushConstraint(oPart1_4?.SurfaceBodies[1].Faces[4], oPart3_4?.SurfaceBodies[1].Faces[3], 0);
                var ofConstr29 = oFaceDef?.Constraints.AddMateConstraint(oPart1_4?.SurfaceBodies[1].Faces[10], oPart3_4?.SurfaceBodies[1].Faces[4], 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);
                ofConstr29?.ConvertToFlushConstraint(oPart1_4?.SurfaceBodies[1].Faces[10], oPart3_4?.SurfaceBodies[1].Faces[4], 0);
            }
        }
    }
}
