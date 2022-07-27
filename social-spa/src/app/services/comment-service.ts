import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Comment } from '../models/comment';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  private baseUrl = `${environment.apiURL}/api/social/postcomment`;

  constructor(private http: HttpClient) {}

  //headers: { 'Content-Type': 'multipart/form-data' }
  addComment(commentModel: Comment): any {
    return this.http.post<any>(this.baseUrl, commentModel);
  }

  getComments(postId: number): any {
    return this.http.get<any>(this.baseUrl + '/' + postId);
  }

  editComment(commentId: any, newComment: string) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json-patch+json',
      }),
    };

    return this.http.patch(
      this.baseUrl + '/' + commentId,
      [
        {
          op: 'replace',
          path: '/comment',
          value: newComment,
        },
      ],
      options
    );
  }

  deleteComment(commentId: any) {
    return this.http.delete<any>(this.baseUrl + '/' + commentId);
  }
}
