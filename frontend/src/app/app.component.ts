import {Component, OnInit, ViewChild} from '@angular/core';
import {AuthService} from "./modules/auth/services/auth.service";
import {UserRole} from "./models/user";
import {Router} from "@angular/router";
import {MatSidenav} from "@angular/material/sidenav";
import {IUser} from "./interfaces/user";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  @ViewChild('sidenav')
  sidenav!: MatSidenav;
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;

  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit() {
    this.authService.currentUser$
      .subscribe((user: IUser | null) => {
        this.isLoggedIn = !!user;
        if (this.isLoggedIn) {
          this.isAdmin = this.authService.checkRole(UserRole.Admin);
        }
      });
  }

  logOut() {
    this.authService.logout();
    this.router.navigateByUrl('/refresh', {skipLocationChange: true})
      .then(() => this.router.navigate(['/']));
  }

  toggleSidenav(): void {
    this.sidenav.toggle();
  }
}
