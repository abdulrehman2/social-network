import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  private baseUrl = `${environment.apiURL}/api/social/post`;

  constructor(private http: HttpClient) {}

  //headers: { 'Content-Type': 'multipart/form-data' }
  addPost(postModel: any): any {
    return this.http.post<any>(this.baseUrl, postModel);
  }
  getPosts(userId: any): any {
    return this.http.get<any>(this.baseUrl + '/user_posts/' + userId);
  }

  getUserWall(): any {
    return this.http.get<any>(this.baseUrl + '/user_wall');
  }
}
