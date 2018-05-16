import { Component, OnInit } from '@angular/core';
import * as ClientOAuth2 from 'client-oauth2';

@Component({
  selector: 'login-component',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  auth: ClientOAuth2;
  token: any;

  ngOnInit() {
    this.auth = new ClientOAuth2({
      clientId: 'js_angular_oauth_client',
      clientSecret: 'SENG',
      accessTokenUri: 'http://localhost:5000/connect/token',
      authorizationUri: 'http://localhost:5000/connect/authorize',
      redirectUri: 'http://localhost:4204/login',
      scopes: ['api:system'],
    });
  }

  api() {

    const url = 'http://localhost:5001/api/v1/Identity';
    const xhr = new XMLHttpRequest();
    xhr.open('GET', url);
    xhr.onload = () => {
      console.log(xhr.status, JSON.parse(xhr.responseText));
    };
    this.auth.credentials.getToken()
      .then((data: any) => {
        xhr.setRequestHeader('accept', 'application/json');
        xhr.setRequestHeader('authorization', 'Bearer ' + data.accessToken);
        xhr.send();
      });
  }

  logout() {
    //
  }
}
