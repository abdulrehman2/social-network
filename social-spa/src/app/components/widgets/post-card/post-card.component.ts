import {
  Component,
  Input,
  OnInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
} from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MenuItem } from 'primeng/api/menuitem';
import { Post } from 'src/app/models/post';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss'],
  providers: [ConfirmationService],
})
export class PostCardComponent implements OnInit {
  @Input() post: Post;
  showComments: boolean = false;
  constructor(public cd: ChangeDetectorRef) {}

  ngOnInit(): void {}

  isImage(url: any) {
    return /\.(jpg|jpeg|png|webp|avif|gif|svg)$/.test(url);
  }
  isVideo(url: any) {
    return /\.(webm|mpg|mp2|mpeg|mp4|avi|wmv)$/.test(url);
  }
}
