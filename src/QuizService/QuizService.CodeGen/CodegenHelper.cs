using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TypeLite;

namespace QuizService.CodeGen
{
    /// <summary>
    /// Provides methods for generation of the Typescript code from the C# code.
    /// </summary>
    public static class CodegenHelper
    {
        /// <summary>
        /// Gets definitions of classes.
        /// </summary>
        /// <returns>Typescript definitions of C# classes.</returns>
        public static TypeScriptFluent GetClassDefinitions()
        {
            var modelElements = GetTypesForCodeGeneration().Where(t => t.IsInterface || t.IsClass);
            return SetupDefinitions(modelElements);
        }

        /// <summary>
        /// Gets definitions of enumerations.
        /// </summary>
        /// <returns>Typescript definitions of C# enumerations.</returns>
        public static TypeScriptFluent GetEnumDefinitions()
        {
            var modelElements = GetTypesForCodeGeneration().Where(t => t.IsEnum);
            return SetupDefinitions(modelElements);
        }

        /// <summary>
        /// Modifies generated Typescript code.
        /// </summary>
        /// <param name="rawOutput">Generated Typescript code.</param>
        /// <returns>Modified Typescript code.</returns>
        public static string PostProcess(string rawOutput)
        {
            rawOutput = Regex.Replace(rawOutput, @"interface ", "export interface ");
            rawOutput = Regex.Replace(rawOutput, @"interface (?=[^I])", "class ");
            rawOutput = Regex.Replace(rawOutput, @"[.\w]+\.(\w+)", "$1");
            return rawOutput;
        }

        private static IEnumerable<Type> GetTypesForCodeGeneration()
        {
            Assembly modelAssembly = typeof(Model.Quiz).GetTypeInfo().Assembly;
            return modelAssembly.GetExportedTypes()
                                .Where(t => t.Namespace.StartsWith("QuizService.Model"));
        }

        private static TypeScriptFluent SetupDefinitions(IEnumerable<Type> modelElements)
        {
            var definitions = new TypeScriptFluent();
            foreach (Type modelElement in modelElements)
            {
                definitions.For(modelElement).ToModule(String.Empty);
            }
            return definitions.WithMemberFormatter((identifier) =>
                                Char.ToLower(identifier.Name[0]) + identifier.Name.Substring(1)
                               );
        }
    }
}
