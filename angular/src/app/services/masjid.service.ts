import { Masjid }     from '../models/Masjid';
import { Injectable } from '@angular/core';
import { Http }       from '@angular/http';
import { stringify }  from 'querystring';
import { Observable } from 'rxjs/Observable';
import { Config }     from '../models/Config';

import 'rxjs/add/operator/map';

@Injectable()
export class MasjidService {
  private masjids = [];
  private BaseUrl = Config.appBase + 'api/masjid';
  
  constructor(private http : Http ) {}

  getMasjids() {
    return this.http.get(this.BaseUrl + '').map(
      (response) => {
        const s = response.json();
        return s;
      }
    );
  }
}