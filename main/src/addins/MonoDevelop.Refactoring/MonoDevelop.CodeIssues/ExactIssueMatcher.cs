//
// ExactIssueMatcher.cs
//
// Author:
//       Simon Lindgren <simon.n.lindgren@gmail.com>
//
// Copyright (c) 2013 Simon Lindgren
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
using System.Collections.Generic;
using MonoDevelop.CodeActions;
using System.Linq;

namespace MonoDevelop.CodeIssues
{
	public class ExactIssueMatcher: IActionMatcher
	{
		#region IIssueMatcher implementation

		public IEnumerable<IssueMatch> Match (IList<ActionSummary> summaries, IList<CodeAction> realActions)
		{
			var summaryLookup = summaries.ToLookup (summary => summary.Region);
			foreach (var action in realActions) {
				if (summaryLookup.Contains (action.DocumentRegion)) {
					yield return new IssueMatch {
						Action = action,
						Summary = summaryLookup[action.DocumentRegion].First ()
					};
				}
			}
		}

		#endregion
	}
}
