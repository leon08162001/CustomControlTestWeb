/*
	Simple progress bar I found at javascripts.com
*/
var ProgressBar = function() {
    var progressEnd = 9; 	// set to number of progress <span>'s.
    var progressColor = 'blue'; // set to progress bar color
    var progressInterval = 100; // set to time between updates (milli-seconds)

    var progressAt = progressEnd;
    var progressTimer;

    this.update = function progress_update(controlID) {
        window.ProgressBar = this;
        progressAt++;
        if (progressAt > progressEnd) progress_clear(controlID);
        else document.getElementById(controlID + '_progress' + progressAt).style.backgroundColor = progressColor;
        progressTimer = setTimeout("ProgressBar.update('" + controlID + "')", progressInterval);
    };

    this.stop = function progress_stop(controlID) {
        clearTimeout(progressTimer);
        progress_clear(controlID);
    };

    function progress_clear(controlID) {
        for (var i = 1; i <= progressEnd; i++) document.getElementById(controlID + '_progress' + i).style.backgroundColor = 'transparent';
        progressAt = 0;
    }

    function constructor() {

    }

    constructor();
}