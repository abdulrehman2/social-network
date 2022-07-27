import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { UserService } from 'src/app/services/user-service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  userInfo: any = {};
  constructor(
    private userService: UserService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  signupUser() {
    this.userService.singup(this.userInfo).subscribe(
      (response: any) => {
        this.messageService.add({
          key: 'layout',
          severity: 'success',
          summary: response.message,
        });
        this.userService.setUserModel(response.data);
        this.router.navigate(['/home/feed']);
      },
      (error: any) => {
        this.messageService.add({
          key: 'layout',
          severity: 'error',
          summary: error.error.message,
        });
      }
    );
  }
}
