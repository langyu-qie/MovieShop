import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isLoggedIn: boolean = false;
  user!: User;

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {

    this.authService.isLoggedIn.subscribe(resp => this.isLoggedIn = resp);
    this.authService.currentUser.subscribe(resp => this.user = resp);
  }

  logout() {

    this.authService.logout();
    this.router.navigateByUrl('account/login');

  }

}
