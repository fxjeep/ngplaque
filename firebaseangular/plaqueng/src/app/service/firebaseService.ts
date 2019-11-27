import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';

 
@Injectable({
  providedIn: 'root'
})
export class LoginService {
 
  authState: any = null;

  constructor(public afAuth: AngularFireAuth) {
    this.afAuth.authState.subscribe((auth) => { this.authState = auth; });
  }

  isLoggedIn(){
      return this.authState !== null;
  }
  
  login(email: string, password: string) {
    return this.afAuth.auth.signInWithEmailAndPassword(email, password);
  }
  
  async logout() {
    await this.afAuth.auth.signOut()
              .catch(function(error) { 
                alert(error); 
              });
  }

}