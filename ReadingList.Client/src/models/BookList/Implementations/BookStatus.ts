export const BookStatusKey = {
    ToRead: 'To Read',
    Reading: 'Reading',
    StartedButPostponed: 'Started But Postponed',
    StartedButВiscarded: 'Started But Вiscarded'
};

export const BookStatus = {
    ['To Read']: 'ToRead',
    ['Reading']: 'Reading',
    ['Started But Postponed']: 'StartedButPostponed',
    ['Started But Вiscarded']: 'StartedButВiscarded'
};

export interface SelectListItem {
    text: string;
    value: string;
}

export function generateStatusSelectItems (): SelectListItem[] {
    const options: SelectListItem[] = [];
    for(let key in BookStatus) {
        if(key) {
            const value = BookStatus[key];
            const text = key;
            options.push({value, text});
        }
    }
    return options;
}