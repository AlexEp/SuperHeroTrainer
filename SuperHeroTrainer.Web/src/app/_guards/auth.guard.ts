import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot, ActivatedRouteSnapshot, UrlTree } from '@angular/router';

import { AlertService } from '../shared/services/alert.service';
import { Observable } from 'rxjs';
import { AuthService } from '../shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, 
              private router: Router,
              private alertService: AlertService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):   | boolean | UrlTree| Promise<boolean|UrlTree> | Observable<boolean | UrlTree>
   {
    if (this.authService.isLoggedIn()) {
      return true;
    }

    return this.router.createUrlTree(['/login']);
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot):  | boolean | UrlTree| Promise<boolean|UrlTree> | Observable<boolean | UrlTree> {
    return this.canActivate(childRoute,state)
   }
}
