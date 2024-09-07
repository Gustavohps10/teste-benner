export function secondsToHms(duration: number): string {
    var hours = Math.floor(duration / 3600);
    var minutes = Math.floor(duration % 3600 / 60);
    var seconds = Math.floor(duration % 3600 % 60);

    var hourDisplay = (hours < 10 ? "0" : "") + hours;
    var minutsDisplay = (minutes < 10 ? "0" : "") + minutes;
    var secondsDisplay = (seconds < 10 ? "0" : "") + seconds;

    if(hours < 1){
        return minutsDisplay + ":" + secondsDisplay
    }
    return hourDisplay + ":" + minutsDisplay + ":" + secondsDisplay; 
}