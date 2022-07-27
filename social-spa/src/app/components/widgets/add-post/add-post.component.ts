import { Component, OnInit } from '@angular/core';
import { PostToCreate } from 'src/app/models/postToCreate';
import { PostService } from 'src/app/services/post-service';
import { MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/FileUpload';

@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.scss'],
  providers: [MessageService],
})
export class AddPostComponent implements OnInit {
  public post: PostToCreate = {};
  private file: any;
  postDialogVisible: boolean = false;

  constructor(
    private postService: PostService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {}

  addPost(uploader: FileUpload): void {
    debugger;
    let formData: any = new FormData();
    formData.append('media', this.file);
    formData.append('writtenText', this.post.writtenText);

    this.postService.addPost(formData).subscribe(
      (response: any) => {
        this.post = {};
        uploader.clear();
        this.messageService.add({
          key: 'layout',
          severity: 'success',
          summary: 'Post Added Successfully',
          detail: 'Via MessageService',
        });
      },
      (error: any) => {}
    );
  }
  uploadHandler(event: any): void {
    this.file = event.files[0];
  }

  openPostDialog() {
    this.postDialogVisible = true;
  }
}
