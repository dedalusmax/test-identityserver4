import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
    selector: 'logout-component',
    template: ''
})
export class LogoutComponent implements OnInit {

    constructor(
        private oidcSecurityService: OidcSecurityService) { }

    ngOnInit() {
        this.oidcSecurityService.logoff();
    }
}
