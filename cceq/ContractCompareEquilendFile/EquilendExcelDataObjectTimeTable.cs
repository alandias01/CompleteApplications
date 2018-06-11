using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContractCompareEquilendFile
{
    public class EquilendExcelDataObjectTimeTable
    {
        public string EQUILEND_TXN_ID { get; set; }
        public string LEGAL_ENTITY_ID { get; set; }
        public string CPTY_LEGAL_ENTITY_ID { get; set; }
        public string BORROW_LOAN_TYPE_CD { get; set; }
        public string SUBACCOUNT_ID { get; set; }
        public string SECURITY_ID { get; set; }
        public string SECURITY_ID_TYPE_CD { get; set; }
        public string RATE_TYPE_CD { get; set; }
        public string RATE_AMT { get; set; }
        public string FEE_TYPE_CD { get; set; }
        public string FEE_AMT { get; set; }
        public string DIVIDEND_RATE_PCT { get; set; }
        public string DIVIDEND_TRACKING_IND { get; set; }
        public string COLLATERAL_TYPE_CD { get; set; }
        public string BILLING_FX_RATE { get; set; }
        public string BILLING_DERIVATION_IND { get; set; }
        public string COLLATERAL_CURRENCY_CD { get; set; }
        public string CALLABLE_IND { get; set; }
        public string SETTLEMENT_DT { get; set; }
        public string COMPARE_RECORD_TYPE_CD { get; set; }
        public string UNIT_QTY { get; set; }
        public string ORDER_STATE_CD { get; set; }
        public string PREPAY_RATE_PCT { get; set; }
        public string CASH_PAYMENT_AMT { get; set; }
        public string BILLING_VALUE_AMT { get; set; }
        public string BILLING_CURRENCY_CD { get; set; }
        public string COLLATERAL_DESC_CD { get; set; }
        public string CONTRACT_PRICE_AMT { get; set; }
        public string COLLATERAL_MARGIN_PCT { get; set; }
        public string CONTRACT_VALUE_AMT { get; set; }
        public string TRADE_DT { get; set; }
        public string COLLATERAL_DT { get; set; }
        public string TERM_DT { get; set; }
        public string TERM_TYPE_CD { get; set; }
        public string HOLD_DT { get; set; }
        public string CALLABLE_DT { get; set; }
        public string RESET_INTERVAL_DAYS { get; set; }
        public string REBATE_RECEIVABLE_AMT { get; set; }
        public string REBATE_PAYABLE_AMT { get; set; }
        public string FEE_RECEIVABLE_AMT { get; set; }
        public string FEE_PAYABLE_AMT { get; set; }
        public string RATE_ADJUST_DT { get; set; }
        public string BUYIN_DT { get; set; }
        public string TERMINATION_IND { get; set; }
        public string BORROWER_SETTLE_INSTRUC_ID { get; set; }
        public string LENDER_SETTLE_INSTRUC_ID { get; set; }
        public string SETTLEMENT_TYPE_CD { get; set; }
        public string MARKING_PARAMETERS { get; set; }
        public string INTERNAL_REF_ID { get; set; }
        public string COUNTERPARTY_REF_ID { get; set; }
        public string CORPORATE_ACTION_TYPE { get; set; }
        public string EX_DT { get; set; }
        public string RECORD_DT { get; set; }
        public string INTERNAL_CUSTOM_FIELD { get; set; }
        public string EXTERNAL_CUSTOM_FIELD { get; set; }
        public string OLD_EQUILEND_TXN_ID { get; set; }
        public string BILLING_PRICE_AMT { get; set; }
        public string BILLING_MARGIN_PCT { get; set; }
        public string COLLATERAL_VALUE_AMT { get; set; }
        public string EQUILEND_RETURN_ID { get; set; }
        public string RETURN_TRADE_DT { get; set; }
        public string RETURN_SETTLEMENT_DT { get; set; }
        public string EQUILEND_RECALL_ID { get; set; }
        public string RECALL_EFFECTIVE_DT { get; set; }
        public string RECALL_DUE_DT { get; set; }
        public string REASON_CD { get; set; }
        public string TIMETABLE_ID { get; set; }

        public EquilendExcelDataObjectTimeTable()
        {

        }

        public EquilendExcelDataObjectTimeTable(EquilendExcelDataObjectGrid x)
        {
            this.EQUILEND_TXN_ID = x.EQUILEND_TXN_ID;
            this.LEGAL_ENTITY_ID = x.LEGAL_ENTITY_ID;
            this.CPTY_LEGAL_ENTITY_ID = x.CPTY_LEGAL_ENTITY_ID;
            this.BORROW_LOAN_TYPE_CD = x.BORROW_LOAN_TYPE_CD;
            this.SUBACCOUNT_ID = x.SUBACCOUNT_ID;
            this.SECURITY_ID = x.SECURITY_ID;
            this.SECURITY_ID_TYPE_CD = x.SECURITY_ID_TYPE_CD;
            this.RATE_TYPE_CD = x.RATE_TYPE_CD;
            this.RATE_AMT = x.RATE_AMT;
            this.FEE_TYPE_CD = x.FEE_TYPE_CD;
            this.FEE_AMT = x.FEE_AMT;
            this.DIVIDEND_RATE_PCT = x.DIVIDEND_RATE_PCT;
            this.DIVIDEND_TRACKING_IND = x.DIVIDEND_TRACKING_IND;
            this.COLLATERAL_TYPE_CD = x.COLLATERAL_TYPE_CD;
            this.BILLING_FX_RATE = x.BILLING_FX_RATE;
            this.BILLING_DERIVATION_IND = x.BILLING_DERIVATION_IND;
            this.COLLATERAL_CURRENCY_CD = x.COLLATERAL_CURRENCY_CD;
            this.CALLABLE_IND = x.CALLABLE_IND;
            this.SETTLEMENT_DT = x.SETTLEMENT_DT;
            this.COMPARE_RECORD_TYPE_CD = x.COMPARE_RECORD_TYPE_CD;
            this.UNIT_QTY = x.UNIT_QTY;
            this.ORDER_STATE_CD = x.ORDER_STATE_CD;
            this.PREPAY_RATE_PCT = x.PREPAY_RATE_PCT;
            this.CASH_PAYMENT_AMT = x.CASH_PAYMENT_AMT;
            this.BILLING_VALUE_AMT = x.BILLING_VALUE_AMT;
            this.BILLING_CURRENCY_CD = x.BILLING_CURRENCY_CD;
            this.COLLATERAL_DESC_CD = x.COLLATERAL_DESC_CD;
            this.CONTRACT_PRICE_AMT = x.CONTRACT_PRICE_AMT;
            this.COLLATERAL_MARGIN_PCT = x.COLLATERAL_MARGIN_PCT;
            this.CONTRACT_VALUE_AMT = x.CONTRACT_VALUE_AMT;
            this.TRADE_DT = x.TRADE_DT;
            this.COLLATERAL_DT = x.COLLATERAL_DT;
            this.TERM_DT = x.TERM_DT;
            this.TERM_TYPE_CD = x.TERM_TYPE_CD;
            this.HOLD_DT = x.HOLD_DT;
            this.CALLABLE_DT = x.CALLABLE_DT;
            this.RESET_INTERVAL_DAYS = x.RESET_INTERVAL_DAYS;
            this.REBATE_RECEIVABLE_AMT = x.REBATE_RECEIVABLE_AMT;
            this.REBATE_PAYABLE_AMT = x.REBATE_PAYABLE_AMT;
            this.FEE_RECEIVABLE_AMT = x.FEE_RECEIVABLE_AMT;
            this.FEE_PAYABLE_AMT = x.FEE_PAYABLE_AMT;
            this.RATE_ADJUST_DT = x.RATE_ADJUST_DT;
            this.BUYIN_DT = x.BUYIN_DT;
            this.TERMINATION_IND = x.TERMINATION_IND;
            this.BORROWER_SETTLE_INSTRUC_ID = x.BORROWER_SETTLE_INSTRUC_ID;
            this.LENDER_SETTLE_INSTRUC_ID = x.LENDER_SETTLE_INSTRUC_ID;
            this.SETTLEMENT_TYPE_CD = x.SETTLEMENT_TYPE_CD;
            this.MARKING_PARAMETERS = x.MARKING_PARAMETERS;
            this.INTERNAL_REF_ID = x.INTERNAL_REF_ID;
            this.COUNTERPARTY_REF_ID = x.COUNTERPARTY_REF_ID;
            this.CORPORATE_ACTION_TYPE = x.CORPORATE_ACTION_TYPE;
            this.EX_DT = x.EX_DT;
            this.RECORD_DT = x.RECORD_DT;
            this.INTERNAL_CUSTOM_FIELD = x.INTERNAL_CUSTOM_FIELD;
            this.EXTERNAL_CUSTOM_FIELD = x.EXTERNAL_CUSTOM_FIELD;
            this.OLD_EQUILEND_TXN_ID = x.OLD_EQUILEND_TXN_ID;
            this.BILLING_PRICE_AMT = x.BILLING_PRICE_AMT;
            this.BILLING_MARGIN_PCT = x.BILLING_MARGIN_PCT;
            this.COLLATERAL_VALUE_AMT = x.COLLATERAL_VALUE_AMT;
            this.EQUILEND_RETURN_ID = x.EQUILEND_RETURN_ID;
            this.RETURN_TRADE_DT = x.RETURN_TRADE_DT;
            this.RETURN_SETTLEMENT_DT = x.RETURN_SETTLEMENT_DT;
            this.EQUILEND_RECALL_ID = x.EQUILEND_RECALL_ID;
            this.RECALL_EFFECTIVE_DT = x.RECALL_EFFECTIVE_DT;
            this.RECALL_DUE_DT = x.RECALL_DUE_DT;
            this.REASON_CD = x.REASON_CD;
            this.TIMETABLE_ID = x.TIMETABLE_ID;
        }

    }
}


        



