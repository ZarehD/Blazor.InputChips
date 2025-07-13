export async function applyConfig(id, dotnetHelper) {
	if (globalThis.icp == undefined) {
		globalThis.icp = new Promise(function (resolve, reject) {
			const tag = document.createElement("script");
			tag.src = "_content/Blazor.InputChips/Blazor.InputChips.min.js";
			tag.type = "text/javascript";
			tag.onload = function () {
				globalThis.InputChipsLoaded = true;
				resolve();
			};

			document["body"].appendChild(tag);
		});
	}
	await globalThis.icp;
	const ice = document.getElementById(id);
	if (ice != null) {
		ice.addEventListener('keypress', (e) => {
			e.preventDefault();
			const ss = ice.selectionStart;
			const se = ice.selectionEnd;
			dotnetHelper.invokeMethodAsync('SetSelectionIndexAsync', { start: ss, end: se });
		});
	}
}
export async function setCursorPos(id, ss, se) {
	const ice = document.getElementById(id);
	if (ice == null) { return; }
	ice.selectionEnd = se;
	ice.selectionStart = ss;
}
