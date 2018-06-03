export const BookStatus = {
    1: 'To Reading',
    2: 'Reading',
    3: 'Started But Postponed',
    4: 'Read'
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
            options.push({value: key, text});
        }
    }
    return options;
}