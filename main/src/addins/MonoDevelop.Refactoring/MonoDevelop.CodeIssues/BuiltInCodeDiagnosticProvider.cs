﻿//
// BuiltInCodeDiagnosticProvider.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
//
// Copyright (c) 2015 Xamarin Inc. (http://xamarin.com)
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

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using MonoDevelop.CodeIssues;
using MonoDevelop.Core;
using System.Threading.Tasks;
using MonoDevelop.Ide.Editor;
using MonoDevelop.CodeActions;

namespace MonoDevelop.CodeIssues
{
	
	/// <summary>
	/// Provides all IDE code diagnostics and fix provider.
	/// (Scans the app domain for providers)
	/// </summary>
	class BuiltInCodeDiagnosticProvider : CodeDiagnosticProvider
	{
		readonly static Task<AnalyzersFromAssembly> builtInDiagnostics;

		static BuiltInCodeDiagnosticProvider ()
		{
			builtInDiagnostics = Task.Run (() => {
				var result = new AnalyzersFromAssembly ();
				foreach (var asm in AppDomain.CurrentDomain.GetAssemblies()) {
					try {
						result.AddAssembly (asm);
					} catch (Exception e) {
						LoggingService.LogError ("error while loading diagnostics in " + asm.FullName, e);
					}
				}
				return result;
			});
		}

		internal async static Task<IEnumerable<CodeDiagnosticDescriptor>> GetBuiltInCodeIssuesAsync (string language, bool includeDisabledNodes = false, CancellationToken cancellationToken = default (CancellationToken))
		{
			var diags = await builtInDiagnostics.ConfigureAwait (false);
			var builtInCodeDiagnostics = diags.Analyzers;
			if (string.IsNullOrEmpty (language))
				return includeDisabledNodes ? builtInCodeDiagnostics : builtInCodeDiagnostics.Where (act => act.IsEnabled);
			return includeDisabledNodes ? builtInCodeDiagnostics.Where (ca => ca.Languages.Contains (language)) : builtInCodeDiagnostics.Where (ca => ca.Languages.Contains (language) && ca.IsEnabled);
		}

		public async static Task<IEnumerable<CodeFixDescriptor>> GetBuiltInCodeFixDescriptorAsync (string language, CancellationToken cancellationToken = default (CancellationToken))
		{
			var diags = await builtInDiagnostics.ConfigureAwait (false);
			var builtInCodeFixes = diags.Fixes;
			return string.IsNullOrEmpty (language) ? builtInCodeFixes : builtInCodeFixes.Where (cfp => cfp.Languages.Contains (language));
		}

		public async static Task<IEnumerable<CodeRefactoringDescriptor>> GetBuiltInCodeActionsAsync (string language, bool includeDisabledNodes = false, CancellationToken cancellationToken = default (CancellationToken))
		{
			var diags = await builtInDiagnostics.ConfigureAwait (false);
			var builtInCodeFixes = diags.Refactorings;
			return string.IsNullOrEmpty (language) ? builtInCodeFixes : builtInCodeFixes.Where (cfp => cfp.Language.Contains (language));
		}

		public override Task<IEnumerable<CodeFixDescriptor>> GetCodeFixDescriptorAsync (DocumentContext document, string language, CancellationToken cancellationToken)
		{
			return GetBuiltInCodeFixDescriptorAsync (language, cancellationToken);
		}

		public override Task<IEnumerable<CodeDiagnosticDescriptor>> GetCodeIssuesAsync (DocumentContext document, string language, CancellationToken cancellationToken)
		{
			return GetBuiltInCodeIssuesAsync (language, false, cancellationToken);
		}

		public override Task<IEnumerable<CodeRefactoringDescriptor>> GetCodeActionsAsync (DocumentContext document, string language, CancellationToken cancellationToken = default (CancellationToken))
		{
			return GetBuiltInCodeActionsAsync (language, false, cancellationToken);
		}
	}
}