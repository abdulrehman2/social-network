import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private baseUrl = `${environment.apiURL}/api/identity`;

  constructor(private http: HttpClient) {}

  public login(loginModel: any): any {
    return this.http.post<User>(this.baseUrl + '/account/login', loginModel);
  }
  public singup(singupModel: any): any {
    return this.http.post<any>(this.baseUrl + '/account/signup', singupModel);
  }

  public searchUser(query: any): any {
    return this.http.get<any>(this.baseUrl + '/user/search/' + query);
  }

  public getUserDetails(userId: any) {
    return this.http.get<any>(this.baseUrl + '/user/' + userId);
  }

  public getUserModel(): User {
    let model = localStorage.getItem('user');
    return JSON.parse(model);
  }

  public setUserModel(user: User): void {
    localStorage.setItem('jwt', user.token);
    localStorage.setItem('user', JSON.stringify(user));
  }
}
