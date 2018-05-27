export const BookStatus = {
    1: 'To Reading',
    2: 'Reading',
    3: 'Started But Postponed',
    4: 'Started But Вiscarded',
    5: 'Read'
};

export interface SelectListItem {
    text: string;
    value: string;
}

export function generateStatusSelectItems (): SelectListItem[] {
    const options: SelectListItem[] = [];
    for(let key in BookStatus) {
        if(key) {
            const text = BookStatus[key];
            const value = key;
            options.push({value, text});
        }
    }
    return options;
}