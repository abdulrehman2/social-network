import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Comment } from 'src/app/models/comment';
import { CommentToCreate } from 'src/app/models/commentToCreate';
import { Post } from 'src/app/models/post';
import { CommentService } from 'src/app/services/comment-service';

@Component({
  selector: 'app-post-comments',
  templateUrl: './post-comments.component.html',
  styleUrls: ['./post-comments.component.scss'],
})
export class PostCommentsComponent implements OnInit {
  @Input() post: Post;
  @Input() showComments: boolean;
  comments: Comment[] = [];
  commentToAdd: Comment = {};

  constructor(
    private commentService: CommentService,
    public cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.commentService.getComments(this.post.id).subscribe(
      (comments: Comment[]) => {
        debugger;
        this.comments = comments;
        this.cd.detectChanges();
      },
      (error: any) => {}
    );
  }

  addComment(): any {
    this.commentToAdd.postId = this.post.id;
    this.commentService.addComment(this.commentToAdd).subscribe(
      (response: any) => {
        debugger;
        this.comments.unshift(response);
        this.commentToAdd = {};
      },
      (error: any) => {}
    );
  }
  commentDeleteSubscriber(id: any) {
    debugger;
    this.comments = this.comments.filter((comment) => comment.id != id);
  }
}
