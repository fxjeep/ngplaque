export class Utils {
   static NewID():string{
        return 'xxxxyyyy'.replace(/[xy]/g, function(c) {
            var r = Math.random() * 16 | 0;
            var v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    static GetLastPrint():string{
        let now = new Date();  
        let year = now.getFullYear(); let month = now.getMonth()+1; let date = now.getDate();
        return year.toString() + "-" + (month+1).toString().padStart(2, '0') + "-" + date.toString().padStart(2,'0');
    }

    static GetDownloadFileName():string{
        let now = new Date();  
        let year = now.getFullYear(); 
        let month = now.getMonth(); 
        let date = now.getDate();
        let hour = now.getHours();
        let min = now.getMinutes();
        let sec = now.getSeconds();
        return year.toString() + (month+1).toString().padStart(2, '0') 
                                + date.toString().padStart(2,'0')
                                + "_" + hour.toString().padStart(2,'0')
                                + min.toString().padStart(2,"0")
                                + sec.toString().padStart(2, "0");
    }
}