function previewfile() {
	const fileInput = document.querySelector('input[type="file"]');
	const preview = document.querySelector('#WithImg');
	const previewNoImg = document.querySelector('#NoImg');
	const reader = new FileReader();

	function handleEvent(event) {
		if (event.type === "load") {
			if (preview != null) {
				preview.src = reader.result;
			}
			else {
				previewNoImg.src = reader.result;
				previewNoImg.style.display = 'block';
			}
		}
	}
	const selectedFile = fileInput.files[0];
	$("#upload-file-info").html($("#input").val());
	if (selectedFile) {
		reader.addEventListener('load', handleEvent);
		reader.readAsDataURL(selectedFile);
	}
}