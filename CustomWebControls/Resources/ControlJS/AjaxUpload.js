        var t;
        var i=0;
        var maxi;
        var uploadBtn;
        var hdnUploadedFiles;
        //var btnTriggerUploadedFilesEvent;
        var linkTriggerUploadedFilesEvent;
        function upLoad(BtnObj, UploadedFilesObj, TriggerUploadedFilesEventObj) {
            uploadBtn = BtnObj;
            uploadBtn.disabled = true;
            hdnUploadedFiles = UploadedFilesObj;
            //btnTriggerUploadedFilesEvent = TriggerUploadedFilesEventObj;
            linkTriggerUploadedFilesEvent = TriggerUploadedFilesEventObj;
            maxi = window.frames.length - 1;
            var uploadFrame = window.frames[i];
            var progressBarFrame = window.frames[i].frames[0];
            uploadFrame.document.getElementById("btn_upload").click();
            progressBarFrame.document.getElementById("isUploadFinished").value = "false";
            if (hdnUploadedFiles != null)
            {
                var fileUpload = uploadFrame.document.getElementById("fileUpload");
                var fileName = fileUpload.value.split(/[\\ ]+/).pop();
                hdnUploadedFiles.value += fileName + ";";
            }
            t = window.setInterval("checkUploadFinished()", 250);
        }

        function checkUploadFinished() {
            var progressBarFrame = window.frames[i].frames[0];
            if (progressBarFrame.document.getElementById("progress") != null) {
                if (progressBarFrame.document.getElementById("isUploadFinished").value == "true") {
                    if (i < maxi) {
                        i++;
                        var uploadFrame = window.frames[i];
                        progressBarFrame = window.frames[i].frames[0];
                        uploadFrame.document.getElementById("btn_upload").click();
                        progressBarFrame.document.getElementById("isUploadFinished").value = "false";
                        if (hdnUploadedFiles != null) {
                            var fileUpload = uploadFrame.document.getElementById("fileUpload");
                            var fileName = fileUpload.value.split(/[\\ ]+/).pop();
                            hdnUploadedFiles.value += fileName + ";";
                        }
                    }
                    else {
                        clearInterval(t);
                        uploadBtn.disabled = false;
                        //window.alert("UploadedFiles:" + hdnUploadedFiles.value);
                        if (hdnUploadedFiles == null) {
                            window.location.href = window.location.href;
                        }
                        else {
                            //btnTriggerUploadedFilesEvent.click();
                            linkTriggerUploadedFilesEvent.click();
                        }
                    }
                }
            }
        }
        function doProgress(sGuid, callBackMethod) {
            try {
                APTemplate.AjaxUploadHandler.doProgress(sGuid, function (res) { try { callBackMethod(res); } catch (err) { }});
            }
            catch (err) {

            }
        }