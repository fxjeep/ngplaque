import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ContactFilter'
})
export class ContactFilterPipe implements PipeTransform {

  transform(array: any[], filter: string): any {
    if (!array) {
      return array;
    }

    return array.filter(function(contact, index){
        let inName =  (contact.Name && contact.Name.indexOf(filter) >=0 );
        let inCode =  (contact.Code && contact.Code.indexOf(filter.toUpperCase()) >=0 );
        return (inName || inCode);
    });
  }
}
