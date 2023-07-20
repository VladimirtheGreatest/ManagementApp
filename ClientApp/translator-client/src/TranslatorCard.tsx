import React from 'react';
interface TranslatorDto {
    id: number;
    name: string;
    hourlyRate: string;
    creditCardNumber: string;
    status: string;
}

interface Props {
    translator: TranslatorDto;
}

const TranslatorCard = ({ translator }: Props) => {
    return (
        <div className="translator-card">
            <h3>{translator.name}</h3>
            <p>Hourly Rate: ${translator.hourlyRate}</p>
            <p>Credit Card Number: {translator.creditCardNumber}</p>
            <p>Status: {translator.status}</p>
        </div>
    );
};

export default TranslatorCard;
