import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';

 
@Injectable({
  providedIn: 'root'
})
export class LoginService {
 
  constructor(public afAuth: AngularFireAuth) {
  }

  login(email: string, password: string) {
    this.afAuth.auth.signInWithEmailAndPassword(email, password)
      .then(() => {
        // on success populate variables and select items
        alert("logedin");
      })
      .catch((error) => {
        alert(error);
      });
  }
  
  async logout() {
    await this.afAuth.auth.signOut()
              .catch(function(error) { 
                alert(error); 
              });
  }

}