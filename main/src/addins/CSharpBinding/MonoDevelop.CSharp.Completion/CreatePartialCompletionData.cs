//
// CreatePartialCompletionData.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
//
// Copyright (c) 2014 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.Linq;
using MonoDevelop.Ide.CodeCompletion;
using Microsoft.CodeAnalysis;
using MonoDevelop.CSharp.Formatting;
using MonoDevelop.CSharp.Refactoring;
using MonoDevelop.Ide.TypeSystem;
using MonoDevelop.Ide.Editor.Extension;

namespace MonoDevelop.CSharp.Completion
{
	class CreatePartialCompletionData : RoslynSymbolCompletionData
	{
		readonly ITypeSymbol currentType;
		readonly int declarationBegin;

		public bool GenerateBody { get; set; }

		string displayText;
		public override string DisplayText {
			get {
				if (displayText == null) {
					var model = ext.ParsedDocument.GetAst<SemanticModel> ();
					displayText = base.Symbol.ToMinimalDisplayString (model, ext.Editor.CaretOffset, Ambience.LabelFormat);
				}
				return displayText;
			}
		}

		public CreatePartialCompletionData (ICSharpCode.NRefactory6.CSharp.Completion.ICompletionKeyHandler keyHandler, CSharpCompletionTextEditorExtension ext, int declarationBegin, ITypeSymbol currentType, ISymbol member) : base (keyHandler, ext, member)
		{
			this.currentType = currentType;
			this.declarationBegin = declarationBegin;
			this.GenerateBody = true;
		}

		public override void InsertCompletionText (CompletionListWindow window, ref KeyActions ka, KeyDescriptor descriptor)
		{
			var editor = ext.Editor;
			bool isExplicit = false;
			//			if (member.DeclaringTypeDefinition.Kind == TypeKind.Interface) {
			//				foreach (var m in type.Members) {
			//					if (m.Name == member.Name && !m.ReturnType.Equals (member.ReturnType)) {
			//						isExplicit = true;
			//						break;
			//					}
			//				}
			//			}
			//			var resolvedType = type.Resolve (ext.Project).GetDefinition ();
			//			if (ext.Project != null)
			//				generator.PolicyParent = ext.Project.Policies;

			var result = CSharpCodeGenerator.CreatePartialMemberImplementation (currentType, currentType.Locations.First (), Symbol, isExplicit);
			string sb = result.Code.TrimStart ();
			int trimStart = result.Code.Length - sb.Length;
			sb = sb.TrimEnd ();

			var lastRegion = result.BodyRegions.LastOrDefault ();
			var region = lastRegion == null? null
				: new CodeGeneratorBodyRegion (lastRegion.StartOffset - trimStart, lastRegion.EndOffset - trimStart);

			int targetCaretPosition;
			int selectionEndPosition = -1;
			if (region != null && region.IsValid) {
				targetCaretPosition = declarationBegin + region.EndOffset;

			} else {
				targetCaretPosition = declarationBegin + sb.Length;
			}

			editor.ReplaceText (declarationBegin, editor.CaretOffset - declarationBegin, sb);
			if (selectionEndPosition > 0) {
				editor.CaretOffset = selectionEndPosition;
				editor.SetSelection (targetCaretPosition, selectionEndPosition);
			} else {
				editor.CaretOffset = targetCaretPosition;
			}

			OnTheFlyFormatter.Format (editor, ext.DocumentContext, declarationBegin, declarationBegin + sb.Length);
			editor.CaretLine--;
		}
	}
}