	const fileInput = document.querySelector('input[type="file"]');
	const preview = document.querySelector('#WithImg');
	const previewNoImg = document.querySelector('#NoImg');
	const reader = new FileReader();

	function handleEvent(event) {
		if (event.type === "load") {
			if (preview != null) {
				preview.src = reader.result;
			}
			else
			{
				previewNoImg.src = reader.result;
				previewNoImg.style.display = 'block';
			}
		}
	}

	function addListeners(reader) {
		reader.addEventListener('load', handleEvent);
	}

	function handleSelected(e) {
		const selectedFile = fileInput.files[0];
		if (selectedFile) {
		addListeners(reader);
			reader.readAsDataURL(selectedFile);
		}
	}

	fileInput.addEventListener('change', handleSelected);
