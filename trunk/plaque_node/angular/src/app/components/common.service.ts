import { Injectable } from "@angular/core";
import { Http, Response, Headers } from '@angular/http';
import { LoginResult } from "./models";

@Injectable()
export class CommonService {
    // constructor(private http: Http, 
    //     @Inject(REST_URL) private rootUrl: string) { }

    // login(username:string, password:string) : LoginResult {
    //    return this.http.post(this.rootUrl + "/login", {username:username, password:password})
    //      .map((res: LoginResult) => res.json())
    //      .catch(
    //          (error: any) => Observable.throw(error.json().error || 'Server error')
    //     );
    // }
}

