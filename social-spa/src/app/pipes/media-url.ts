import { Pipe, PipeTransform } from '@angular/core';
import { environment } from 'src/environments/environment';
@Pipe({
  name: 'mediaurl',
  pure: true,
})
export class MediaUrlPipe implements PipeTransform {
  transform(media: string) {
    return `${environment.apiURL}/uploads/${media}`;
  }
}
