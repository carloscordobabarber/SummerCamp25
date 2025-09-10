import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserService } from '../services/user/user.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private userService: UserService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const allowedRoles: string[] = route.data['roles'] || [];
    const userRole = this.userService.getRoleFromToken();
    if (userRole && allowedRoles.includes(userRole)) {
      return true;
    }
    // Mensaje educado de redirección
    sessionStorage.setItem('redirectMessage', 'No tienes permisos para acceder a la página solicitada. Has sido redirigido al inicio.');
    this.router.navigate(['']);
    return false;
  }
}
