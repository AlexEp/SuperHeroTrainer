import { Pipe, PipeTransform } from '@angular/core';
import { Hero } from '../models/hero.model';


@Pipe({
  name: 'orderByPower',
  pure:false
})
export class OrderByPowerPipe implements PipeTransform {

  transform(value: Hero[]): Hero[] {

    if (!value) { return value; } // no array
    if (value.length <= 1) { return value; } // array with only one item

    var newArray =  value.sort((a,b) => (b.power-a.power));
    return newArray;
  }

}
