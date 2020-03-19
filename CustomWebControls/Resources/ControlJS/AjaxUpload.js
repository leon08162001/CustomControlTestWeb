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
            var progressPercentFrame = window.frames[i].frames[0];
            uploadFrame.document.getElementById("btn_upload").click();
            if (progressPercentFrame != null) {
                progressPercentFrame.document.getElementById("isUploadFinished").value = "false";
                if (hdnUploadedFiles != null) {
                    var fileUpload = uploadFrame.document.getElementById("fileUpload");
                    var fileName = fileUpload.value.split(/[\\ ]+/).pop();
                    hdnUploadedFiles.value += fileName + ";";
                }
                t = window.setInterval("checkUploadFinishedWithProgressPercent()", 250);
            }
            else {
                uploadFrame.document.getElementById("isUploadFinished").value = "false";
                if (hdnUploadedFiles != null) {
                    var fileUpload = uploadFrame.document.getElementById("fileUpload");
                    var fileName = fileUpload.value.split(/[\\ ]+/).pop();
                    hdnUploadedFiles.value += fileName + ";";
                    t = window.setInterval("checkUploadFinishedWithProgressBar()", 250);
                }
            }
        }

        function checkUploadFinishedWithProgressPercent() {
            var progressPercentFrame = window.frames[i].frames[0];
            if (progressPercentFrame.document.getElementById("progress") != null) {
                if (progressPercentFrame.document.getElementById("isUploadFinished").value == "true") {
                    if (i < maxi) {
                        i++;
                        var uploadFrame = window.frames[i];
                        progressPercentFrame = window.frames[i].frames[0];
                        uploadFrame.document.getElementById("btn_upload").click();
                        progressPercentFrame.document.getElementById("isUploadFinished").value = "false";
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
        function checkUploadFinishedWithProgressBar() {
            var uploadFrame = window.frames[i];
            if (uploadFrame.document.getElementById("isUploadFinished").value == "true") {
                if (i < maxi) {
                    i++;
                    uploadFrame = window.frames[i];
                    uploadFrame.document.getElementById("btn_upload").click();
                    uploadFrame.document.getElementById("isUploadFinished").value = "false";
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
        function doProgress(sGuid, callBackMethod) {
            try {
                APTemplate.AjaxUploadHandler.doProgress(sGuid, function (res) { try { callBackMethod(res); } catch (err) { }});
            }
            catch (err) {

            }
        }