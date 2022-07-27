import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Login } from 'src/app/models/login';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  providers: [MessageService],
})
export class LoginComponent implements OnInit {
  public loginModel: Login = {};

  constructor(
    private router: Router,
    private _userService: UserService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {}

  verifyCredentials(): void {
    debugger;
    this._userService.login(this.loginModel).subscribe(
      (response: any) => {
        debugger;
        this.messageService.add({
          key: 'layout',
          severity: 'success',
          summary: response.message,
        });
        this._userService.setUserModel(response.data);
        this.router.navigate(['/home/feed']);
      },
      (error: any) => {
        debugger;
        this.messageService.add({
          key: 'layout',
          severity: 'error',
          summary: error.error.message,
        });
      }
    );
  }
}
